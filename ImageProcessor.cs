using POC_Leitura_Imagem;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Tesseract;

class ImageProcessor
{
    public static (Bitmap imagemFinal, string mensagemErroSalvo) ProcessarImagem(string caminhoImagem, string caminhoErro = null)
    {
        using (Bitmap imagemOriginal = new Bitmap(caminhoImagem))
        {
            Bitmap imagemSaturada = AumentarSaturacao(imagemOriginal, 1.5f);
            Bitmap imagemProcessada = new Bitmap(imagemSaturada.Width, imagemSaturada.Height, PixelFormat.Format24bppRgb);

            Rectangle rect = new Rectangle(0, 0, imagemSaturada.Width, imagemSaturada.Height);

            BitmapData dataOriginal = imagemSaturada.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dataProcessada = imagemProcessada.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int stride = dataOriginal.Stride;
            int bytes = stride * imagemSaturada.Height;
            byte[] bufferOriginal = new byte[bytes];
            byte[] bufferProcessado = new byte[bytes];

            Marshal.Copy(dataOriginal.Scan0, bufferOriginal, 0, bytes);
            imagemSaturada.UnlockBits(dataOriginal);

            for (int y = 0; y < imagemSaturada.Height; y++)
            {
                for (int x = 0; x < imagemSaturada.Width; x++)
                {
                    int index = y * stride + x * 3;
                    byte b = bufferOriginal[index];
                    byte g = bufferOriginal[index + 1];
                    byte r = bufferOriginal[index + 2];

                    bool ehVermelho = r > 170 && g < 140 && b < 145;
                    byte cor = (byte)(ehVermelho ? 0 : 255);

                    bufferProcessado[index] = cor;
                    bufferProcessado[index + 1] = cor;
                    bufferProcessado[index + 2] = cor;
                }
            }

            Marshal.Copy(bufferProcessado, 0, dataProcessada.Scan0, bytes);
            imagemProcessada.UnlockBits(dataProcessada);

            Bitmap imagemContraste = MelhorarContraste(imagemProcessada);
            Bitmap imagemDilatada = DilatarNumeros(imagemContraste);
            Bitmap imagemSuavizada = SuavizarImagem(imagemDilatada);
            Bitmap imagemFinal = RedimensionarImagem(imagemSuavizada, imagemSuavizada.Width * 3, imagemSuavizada.Height * 3);

            string mensagemImagemProcessada = "";

            //string caminhoTemporario = $@"C:\\Temp\\imagem_processada_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
            //imagemFinal.Save(caminhoTemporario, System.Drawing.Imaging.ImageFormat.Jpeg);
            //Console.WriteLine($"Imagem processada salva em: {caminhoTemporario}");

            if (!string.IsNullOrEmpty(caminhoErro))
            {
                string nomeArquivo = $"imagem_sem_numero_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
                string caminhoCompleto = Path.Combine(caminhoErro, nomeArquivo);

                imagemFinal.Save(caminhoCompleto, System.Drawing.Imaging.ImageFormat.Jpeg);
                mensagemImagemProcessada = $"🖼️ Imagem com erro salva em: {caminhoCompleto}";
                Console.WriteLine(mensagemImagemProcessada);
            }

            return (imagemFinal, mensagemImagemProcessada);
        }
    }

    private static Bitmap AumentarSaturacao(Bitmap img, float fator)
    {
        Bitmap novaImagem = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
        Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);

        BitmapData dataOriginal = img.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
        BitmapData dataNova = novaImagem.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

        int stride = dataOriginal.Stride;
        int bytes = stride * img.Height;
        byte[] buffer = new byte[bytes];

        Marshal.Copy(dataOriginal.Scan0, buffer, 0, bytes);

        for (int y = 0; y < img.Height; y++)
        {
            for (int x = 0; x < img.Width; x++)
            {
                int index = y * stride + x * 3;
                byte b = buffer[index];
                byte g = buffer[index + 1];
                byte r = buffer[index + 2];

                Color cor = Color.FromArgb(r, g, b);
                float hue = cor.GetHue();
                float sat = Math.Min(cor.GetSaturation() * fator, 1.0f);
                float bright = cor.GetBrightness();

                Color novaCor = FromAhsb(255, hue, sat, bright);

                buffer[index] = novaCor.B;
                buffer[index + 1] = novaCor.G;
                buffer[index + 2] = novaCor.R;
            }
        }

        Marshal.Copy(buffer, 0, dataNova.Scan0, bytes);
        img.UnlockBits(dataOriginal);
        novaImagem.UnlockBits(dataNova);

        return novaImagem;
    }

    private static Color FromAhsb(int a, float h, float s, float b)
    {
        if (s == 0)
        {
            int v = (int)(b * 255);
            return Color.FromArgb(a, v, v, v);
        }

        float fMax, fMid, fMin;
        int iSextant;
        if (b < 0.5)
        {
            fMax = b + b * s;
        }
        else
        {
            fMax = b + s - b * s;
        }

        fMin = 2 * b - fMax;

        h /= 60f;
        iSextant = (int)Math.Floor(h);
        float f = h - iSextant;
        if ((iSextant & 1) == 0)
        {
            fMid = fMin + f * (fMax - fMin);
        }
        else
        {
            fMid = fMax - f * (fMax - fMin);
        }

        int iMax = (int)(fMax * 255);
        int iMid = (int)(fMid * 255);
        int iMin = (int)(fMin * 255);

        switch (iSextant)
        {
            case 0: return Color.FromArgb(a, iMax, iMid, iMin);
            case 1: return Color.FromArgb(a, iMid, iMax, iMin);
            case 2: return Color.FromArgb(a, iMin, iMax, iMid);
            case 3: return Color.FromArgb(a, iMin, iMid, iMax);
            case 4: return Color.FromArgb(a, iMid, iMin, iMax);
            case 5: return Color.FromArgb(a, iMax, iMin, iMid);
            default: return Color.FromArgb(a, 0, 0, 0);
        }
    }

    private static Bitmap MelhorarContraste(Bitmap img)
    {
        Bitmap novaImagem = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);

        Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);

        BitmapData dataOriginal = img.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
        BitmapData dataNova = novaImagem.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

        int stride = dataOriginal.Stride;
        int bytes = stride * img.Height;

        byte[] bufferOriginal = new byte[bytes];
        byte[] bufferNova = new byte[bytes];

        System.Runtime.InteropServices.Marshal.Copy(dataOriginal.Scan0, bufferOriginal, 0, bytes);
        img.UnlockBits(dataOriginal);

        int limiar = CalcularLimiarOtsu(bufferOriginal, img.Width, img.Height, stride);

        for (int y = 0; y < img.Height; y++)
        {
            for (int x = 0; x < img.Width; x++)
            {
                int index = y * stride + x * 3;

                byte b = bufferOriginal[index];
                byte g = bufferOriginal[index + 1];
                byte r = bufferOriginal[index + 2];

                int media = (r + g + b) / 3;
                byte cor = (byte)(media > limiar ? 255 : 0);

                bufferNova[index] = cor;
                bufferNova[index + 1] = cor;
                bufferNova[index + 2] = cor;
            }
        }

        System.Runtime.InteropServices.Marshal.Copy(bufferNova, 0, dataNova.Scan0, bytes);
        novaImagem.UnlockBits(dataNova);

        return novaImagem;
    }

    private static int CalcularLimiarOtsu(byte[] buffer, int width, int height, int stride)
    {
        int[] histograma = new int[256];
        int totalPixels = width * height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * stride + x * 3;
                byte b = buffer[index];
                byte g = buffer[index + 1];
                byte r = buffer[index + 2];
                int media = (r + g + b) / 3;
                histograma[media]++;
            }
        }

        float somaTotal = 0;
        for (int i = 0; i < 256; i++)
            somaTotal += i * histograma[i];

        float somaB = 0;
        int pesoB = 0;
        int pesoF = 0;

        float varMax = 0;
        int limiar = 0;

        for (int t = 0; t < 256; t++)
        {
            pesoB += histograma[t];
            if (pesoB == 0) continue;

            pesoF = totalPixels - pesoB;
            if (pesoF == 0) break;

            somaB += t * histograma[t];

            float mediaB = somaB / pesoB;
            float mediaF = (somaTotal - somaB) / pesoF;

            float varEntre = pesoB * pesoF * (mediaB - mediaF) * (mediaB - mediaF);

            if (varEntre > varMax)
            {
                varMax = varEntre;
                limiar = t;
            }
        }

        return limiar;
    }

    private static Bitmap DilatarNumeros(Bitmap img)
    {
        int largura = img.Width;
        int altura = img.Height;

        Bitmap novaImagem = new Bitmap(largura, altura, PixelFormat.Format24bppRgb);

        Rectangle rect = new Rectangle(0, 0, largura, altura);

        BitmapData dataOrig = img.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
        BitmapData dataNova = novaImagem.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

        int stride = dataOrig.Stride;
        int bytes = stride * altura;

        byte[] bufferOrig = new byte[bytes];
        byte[] bufferNova = new byte[bytes];

        System.Runtime.InteropServices.Marshal.Copy(dataOrig.Scan0, bufferOrig, 0, bytes);
        img.UnlockBits(dataOrig);

        for (int i = 0; i < bufferNova.Length; i++)
            bufferNova[i] = 255;

        for (int y = 2; y < altura - 2; y++)
        {
            for (int x = 2; x < largura - 2; x++)
            {
                int index = y * stride + x * 3;

                byte b = bufferOrig[index];
                byte g = bufferOrig[index + 1];
                byte r = bufferOrig[index + 2];

                if (r == 0 && g == 0 && b == 0)
                {
                    for (int dy = -2; dy <= 2; dy++)
                    {
                        for (int dx = -2; dx <= 2; dx++)
                        {
                            int nx = x + dx;
                            int ny = y + dy;
                            int idx = ny * stride + nx * 3;

                            if (idx >= 0 && idx + 2 < bufferNova.Length)
                            {
                                bufferNova[idx] = 0;
                                bufferNova[idx + 1] = 0;
                                bufferNova[idx + 2] = 0;
                            }
                        }
                    }
                }
            }
        }

        System.Runtime.InteropServices.Marshal.Copy(bufferNova, 0, dataNova.Scan0, bytes);
        novaImagem.UnlockBits(dataNova);

        return novaImagem;
    }

    private static Bitmap SuavizarImagem(Bitmap img)
    {
        Bitmap novaImagem = new Bitmap(img.Width, img.Height);

        using (Graphics g = Graphics.FromImage(novaImagem))
        {
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

            g.DrawImage(img, 0, 0, img.Width, img.Height);
        }

        return novaImagem;
    }

    private static Bitmap RedimensionarImagem(Bitmap img, int novoLargura, int novaAltura)
    {
        Bitmap novaImagem = new Bitmap(novoLargura, novaAltura);
        using (Graphics g = Graphics.FromImage(novaImagem))
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawImage(img, 0, 0, novoLargura, novaAltura);
        }
        return novaImagem;
    }

    public static ResultadoOCR ExtrairNumeroOCR(Bitmap imagem)
    {
        var resultadoOCR = new ResultadoOCR();
        string resultado = "";

        using (var engine = new TesseractEngine(@"C:\\Program Files\\Tesseract-OCR\\tessdata", "eng", EngineMode.Default))
        {
            engine.SetVariable("tessedit_char_whitelist", "0123456789");

            using (var img = PixConverter.ToPix(imagem))
            {
                using (var page = engine.Process(img, PageSegMode.SingleBlock))
                {
                    using (var iterator = page.GetIterator())
                    {
                        iterator.Begin();

                        do
                        {
                            string simbolo = iterator.GetText(PageIteratorLevel.Symbol);
                            float confianca = iterator.GetConfidence(PageIteratorLevel.Symbol);

                            if (!string.IsNullOrWhiteSpace(simbolo) && char.IsDigit(simbolo.First()) && confianca >= 97)
                            {
                                resultado += simbolo;
                            }
                            else
                            {
                                resultadoOCR.Erro = "Caractere com baixa confiança ou inválido.";
                                return resultadoOCR;
                            }
                        }
                        while (iterator.Next(PageIteratorLevel.Symbol));
                    }
                }
            }
        }

        imagem.Dispose();

        if (resultado.Length > 8)
        {
            resultadoOCR.Erro = "Auto identificado com mais de 8 dígitos.";
            return resultadoOCR;
        }

        resultadoOCR.Numero = resultado;
        return resultadoOCR;
    }

    private static string FiltrarNumero(string texto)
    {
        return new string(texto.Where(c => char.IsDigit(c)).ToArray());
    }

    private static string CorrigirErroOCR(string texto)
    {
        return texto
            .Replace("/", "7")
            .Replace("S", "5")
            .Replace("s", "5")
            .Replace("B", "8")
            .Replace("O", "0")
            .Replace("o", "0")
            .Replace("I", "1")
            .Replace("l", "1");
    }
}