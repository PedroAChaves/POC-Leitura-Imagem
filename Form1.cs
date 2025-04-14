using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POC_Leitura_Imagem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelecionarPasta_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtOrigem.Text = folderDialog.SelectedPath; 
                }
            }
        }

        private async void btnRealizarOperacao_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtNumeroLote.Text, out int numeroLote))
            {
                MessageBox.Show("Número do lote inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var repo = new RegistroOCRRepository();
            var autosValidos = repo.ObterAutosPorLote(numeroLote);

            string caminhoPasta = txtOrigem.Text;
            string destinoLog = txtDestinoLog.Text;
            string destinoImagem = txtDestinoImagem.Text;
            int count = 0;
            int acertos = 0;
            string logTexto = "";

            if (string.IsNullOrEmpty(caminhoPasta) || !Directory.Exists(caminhoPasta))
            {
                MessageBox.Show("Selecione uma pasta válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var stopwatchGeral = new Stopwatch();
                stopwatchGeral.Start();

                string[] arquivos = Directory.GetFiles(caminhoPasta, "*.*")
                                            .Where(f => f.ToLower().EndsWith(".jpg") ||
                                                        f.ToLower().EndsWith(".jpeg") ||
                                                        f.ToLower().EndsWith(".png") ||
                                                        f.ToLower().EndsWith(".bmp"))
                                            .ToArray();

                if (arquivos.Length == 0)
                {
                    MessageBox.Show("Nenhuma imagem encontrada na pasta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (arquivos.Length > 100)
                {
                    MessageBox.Show("Não é possível realizar o processamento de mais de 100 arquivos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                progressBar.Value = 0;
                progressBar.Maximum = arquivos.Length;
                logSaida.AppendText($"📂 Processando {arquivos.Length} arquivos na pasta: {caminhoPasta}" + Environment.NewLine);

                foreach (string caminhoImagem in arquivos)
                {
                    count++;
                    string mensagemArquivo = $"\n📂 Analisando arquivo ({count}/{arquivos.Length}): {Path.GetFileName(caminhoImagem)}";
                    logSaida.AppendText(mensagemArquivo + Environment.NewLine);
                    logTexto += mensagemArquivo + Environment.NewLine;

                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    try
                    {
                        (var imagemEditada, _) = await Task.Run(() => ImageProcessor.ProcessarImagem(caminhoImagem));
                        string msgSalvo = "";
                        ResultadoOCR resultadoOCR = await Task.Run(() => ImageProcessor.ExtrairNumeroOCR(imagemEditada));

                        if (resultadoOCR.Sucesso)
                        {
                            if (int.TryParse(resultadoOCR.Numero, out int numeroExtraido))
                            {
                                if (!autosValidos.Contains(numeroExtraido))
                                {
                                    string mensagemErro = $"❌ Auto inválido: {numeroExtraido} não pertence ao lote {numeroLote}.";
                                    logSaida.AppendText(mensagemErro + Environment.NewLine);
                                    logTexto += mensagemErro + Environment.NewLine;

                                    (_, msgSalvo) = await Task.Run(() => ImageProcessor.ProcessarImagem(caminhoImagem, destinoImagem));
                                    if (!string.IsNullOrEmpty(msgSalvo))
                                    {
                                        logSaida.AppendText(msgSalvo + Environment.NewLine);
                                        logTexto += msgSalvo + Environment.NewLine;
                                    }

                                    continue;
                                }
                            }

                            acertos++;
                            string mensagemSucesso = $"✅ Número extraído: {resultadoOCR.Numero}";
                            logSaida.AppendText(mensagemSucesso + Environment.NewLine);
                            logTexto += mensagemSucesso + Environment.NewLine;

                            string pasta = Path.GetDirectoryName(caminhoImagem);
                            string novaImagem = Path.Combine(pasta, resultadoOCR.Numero + Path.GetExtension(caminhoImagem));

                            if (!File.Exists(novaImagem))
                            {
                                File.Move(caminhoImagem, novaImagem);
                                string mensagemRenomeado = $"📌 Arquivo renomeado para: {novaImagem}";
                                logSaida.AppendText(mensagemRenomeado + Environment.NewLine);
                                logTexto += mensagemRenomeado + Environment.NewLine;
                            }
                            else
                            {
                                string extensao = Path.GetExtension(caminhoImagem);
                                string nomeConflito1 = $"CONFLITO_{resultadoOCR.Numero}_1{extensao}";
                                string nomeConflito2 = $"CONFLITO_{resultadoOCR.Numero}_2{extensao}";
                                string caminhoConflito1 = Path.Combine(Path.GetDirectoryName(novaImagem), nomeConflito1);
                                string caminhoConflito2 = Path.Combine(Path.GetDirectoryName(novaImagem), nomeConflito2);

                                File.Move(novaImagem, caminhoConflito1);
                                File.Move(caminhoImagem, caminhoConflito2);

                                string mensagemConflito = $"🚨 CONFLITO CRÍTICO: O número {resultadoOCR.Numero} já existia como nome de outro arquivo. Ambos foram renomeados como CONFLITO.";
                                logSaida.AppendText(mensagemConflito + Environment.NewLine);
                                logTexto += mensagemConflito + Environment.NewLine;

                                logSaida.AppendText($"📁 Arquivos: {nomeConflito1} e {nomeConflito2}" + Environment.NewLine);
                                logTexto += $"📁 Arquivos: {nomeConflito1} e {nomeConflito2}" + Environment.NewLine;
                            }
                        }
                        else
                        {
                            string mensagemErro = $"❌ Falha na leitura: {resultadoOCR.Erro}";
                            logSaida.AppendText(mensagemErro + Environment.NewLine);
                            logTexto += mensagemErro + Environment.NewLine;

                            (_, msgSalvo) = await Task.Run(() => ImageProcessor.ProcessarImagem(caminhoImagem, destinoImagem));
                            if (!string.IsNullOrEmpty(msgSalvo))
                            {
                                logSaida.AppendText(msgSalvo + Environment.NewLine);
                                logTexto += msgSalvo + Environment.NewLine;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string erro = $"❌ Erro ao processar o arquivo {Path.GetFileName(caminhoImagem)}: {ex.Message}";
                        logSaida.AppendText(erro + Environment.NewLine);
                        logTexto += erro + Environment.NewLine;
                    }

                    stopwatch.Stop();
                    string tempoGasto = $"⏱️ Tempo de processamento: {stopwatch.Elapsed.TotalSeconds:F2} segundos";
                    logSaida.AppendText(tempoGasto + Environment.NewLine);
                    logTexto += tempoGasto + Environment.NewLine;

                    progressBar.Value = count;
                }

                logSaida.AppendText(Environment.NewLine + "✅ Processamento concluído!" + Environment.NewLine);
                logTexto += Environment.NewLine + "✅ Processamento concluído!" + Environment.NewLine;

                stopwatchGeral.Stop();
                TimeSpan tempoTotalSpan = stopwatchGeral.Elapsed;
                string tempoTotal = $"🕒 Tempo total de processamento: {tempoTotalSpan.Minutes} min {tempoTotalSpan.Seconds} s";
                logSaida.AppendText(tempoTotal + Environment.NewLine);
                logTexto += tempoTotal + Environment.NewLine;

                double porcentagemAcerto = ((double)acertos / arquivos.Length) * 100;
                double arquivosErros = (arquivos.Length - (double)acertos) ;
                string resumoFinal = $"📊 Arquivos lidos: {arquivos.Length} | Não identificados: {arquivosErros}";
                string resumoAcertos = $"✅ Porcentagem de leitura: ({porcentagemAcerto:F2}%)";

                logSaida.AppendText(Environment.NewLine + resumoFinal + Environment.NewLine);
                logTexto += Environment.NewLine + resumoFinal + Environment.NewLine;
                logSaida.AppendText(Environment.NewLine + resumoAcertos + Environment.NewLine);
                logTexto += Environment.NewLine + resumoAcertos + Environment.NewLine;

                progressBar.Value = progressBar.Maximum;
                progressBar.Value = 0;

                if (!string.IsNullOrEmpty(destinoLog) && Directory.Exists(destinoLog))
                {
                    string nomeArquivoLog = $"log_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                    string caminhoCompletoLog = Path.Combine(destinoLog, nomeArquivoLog);

                    File.WriteAllText(caminhoCompletoLog, logTexto);
                    logSaida.AppendText($"📝 Log salvo em: {caminhoCompletoLog}" + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❗ Erro ao processar as imagens: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtOrigem.Clear();
            txtDestinoLog.Clear();
            txtDestinoImagem.Clear();
            txtNumeroLote.Clear();
            logSaida.Clear();
            progressBar.Value = 0;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSelecionarPastaLog_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDestinoLog.Text = folderDialog.SelectedPath; 
                }
            }
        }

        private void btnSelecionarPastaImagem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDestinoImagem.Text = folderDialog.SelectedPath;
                }
            }
        }
    }
}