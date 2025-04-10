namespace POC_Leitura_Imagem
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelecionarPasta = new System.Windows.Forms.Button();
            this.txtOrigem = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.logSaida = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnRealizarOperacao = new System.Windows.Forms.Button();
            this.btnSelecionarPastaLog = new System.Windows.Forms.Button();
            this.txtDestinoLog = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelecionarPastaImagem = new System.Windows.Forms.Button();
            this.txtDestinoImagem = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIntervalo1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIntervalo2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(483, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "POC - Renomeação automática de imagens";
            // 
            // btnSelecionarPasta
            // 
            this.btnSelecionarPasta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionarPasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionarPasta.Location = new System.Drawing.Point(496, 156);
            this.btnSelecionarPasta.Name = "btnSelecionarPasta";
            this.btnSelecionarPasta.Size = new System.Drawing.Size(76, 28);
            this.btnSelecionarPasta.TabIndex = 14;
            this.btnSelecionarPasta.Text = ". . .";
            this.btnSelecionarPasta.UseVisualStyleBackColor = true;
            this.btnSelecionarPasta.Click += new System.EventHandler(this.btnSelecionarPasta_Click);
            // 
            // txtOrigem
            // 
            this.txtOrigem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrigem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrigem.Location = new System.Drawing.Point(12, 156);
            this.txtOrigem.Multiline = true;
            this.txtOrigem.Name = "txtOrigem";
            this.txtOrigem.Size = new System.Drawing.Size(488, 28);
            this.txtOrigem.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(327, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Selecione o caminho de origem dos arquivos:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 412);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 24);
            this.label3.TabIndex = 41;
            this.label3.Text = "Log de saída:";
            // 
            // logSaida
            // 
            this.logSaida.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logSaida.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logSaida.Location = new System.Drawing.Point(12, 439);
            this.logSaida.Multiline = true;
            this.logSaida.Name = "logSaida";
            this.logSaida.ReadOnly = true;
            this.logSaida.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logSaida.Size = new System.Drawing.Size(560, 112);
            this.logSaida.TabIndex = 40;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(177, 355);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(258, 31);
            this.progressBar.TabIndex = 39;
            // 
            // btnLimpar
            // 
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.Location = new System.Drawing.Point(441, 355);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(69, 31);
            this.btnLimpar.TabIndex = 38;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnSair
            // 
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Location = new System.Drawing.Point(516, 355);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(56, 31);
            this.btnSair.TabIndex = 37;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnRealizarOperacao
            // 
            this.btnRealizarOperacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRealizarOperacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarOperacao.Location = new System.Drawing.Point(12, 355);
            this.btnRealizarOperacao.Name = "btnRealizarOperacao";
            this.btnRealizarOperacao.Size = new System.Drawing.Size(159, 31);
            this.btnRealizarOperacao.TabIndex = 36;
            this.btnRealizarOperacao.Text = "Realizar Operação";
            this.btnRealizarOperacao.UseVisualStyleBackColor = true;
            this.btnRealizarOperacao.Click += new System.EventHandler(this.btnRealizarOperacao_Click);
            // 
            // btnSelecionarPastaLog
            // 
            this.btnSelecionarPastaLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionarPastaLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionarPastaLog.Location = new System.Drawing.Point(496, 226);
            this.btnSelecionarPastaLog.Name = "btnSelecionarPastaLog";
            this.btnSelecionarPastaLog.Size = new System.Drawing.Size(76, 28);
            this.btnSelecionarPastaLog.TabIndex = 44;
            this.btnSelecionarPastaLog.Text = ". . .";
            this.btnSelecionarPastaLog.UseVisualStyleBackColor = true;
            this.btnSelecionarPastaLog.Click += new System.EventHandler(this.btnSelecionarPastaLog_Click);
            // 
            // txtDestinoLog
            // 
            this.txtDestinoLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDestinoLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestinoLog.Location = new System.Drawing.Point(12, 226);
            this.txtDestinoLog.Multiline = true;
            this.txtDestinoLog.Name = "txtDestinoLog";
            this.txtDestinoLog.Size = new System.Drawing.Size(488, 28);
            this.txtDestinoLog.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(329, 20);
            this.label2.TabIndex = 42;
            this.label2.Text = "Caso deseje salvar o log selecione o diretório:";
            // 
            // btnSelecionarPastaImagem
            // 
            this.btnSelecionarPastaImagem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionarPastaImagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionarPastaImagem.Location = new System.Drawing.Point(496, 294);
            this.btnSelecionarPastaImagem.Name = "btnSelecionarPastaImagem";
            this.btnSelecionarPastaImagem.Size = new System.Drawing.Size(76, 28);
            this.btnSelecionarPastaImagem.TabIndex = 47;
            this.btnSelecionarPastaImagem.Text = ". . .";
            this.btnSelecionarPastaImagem.UseVisualStyleBackColor = true;
            this.btnSelecionarPastaImagem.Click += new System.EventHandler(this.btnSelecionarPastaImagem_Click);
            // 
            // txtDestinoImagem
            // 
            this.txtDestinoImagem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDestinoImagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestinoImagem.Location = new System.Drawing.Point(12, 294);
            this.txtDestinoImagem.Multiline = true;
            this.txtDestinoImagem.Name = "txtDestinoImagem";
            this.txtDestinoImagem.Size = new System.Drawing.Size(488, 28);
            this.txtDestinoImagem.TabIndex = 46;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(467, 20);
            this.label5.TabIndex = 45;
            this.label5.Text = "Selecione o caminho das imagens processadas em caso de erro:";
            // 
            // txtIntervalo1
            // 
            this.txtIntervalo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIntervalo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIntervalo1.Location = new System.Drawing.Point(12, 93);
            this.txtIntervalo1.Multiline = true;
            this.txtIntervalo1.Name = "txtIntervalo1";
            this.txtIntervalo1.Size = new System.Drawing.Size(119, 28);
            this.txtIntervalo1.TabIndex = 49;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(243, 20);
            this.label6.TabIndex = 48;
            this.label6.Text = "Intervalo de auto a ser analisado:";
            // 
            // txtIntervalo2
            // 
            this.txtIntervalo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIntervalo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIntervalo2.Location = new System.Drawing.Point(161, 93);
            this.txtIntervalo2.Multiline = true;
            this.txtIntervalo2.Name = "txtIntervalo2";
            this.txtIntervalo2.Size = new System.Drawing.Size(119, 28);
            this.txtIntervalo2.TabIndex = 50;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(137, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 20);
            this.label7.TabIndex = 51;
            this.label7.Text = "a";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 567);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtIntervalo2);
            this.Controls.Add(this.txtIntervalo1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSelecionarPastaImagem);
            this.Controls.Add(this.txtDestinoImagem);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSelecionarPastaLog);
            this.Controls.Add(this.txtDestinoLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logSaida);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnRealizarOperacao);
            this.Controls.Add(this.btnSelecionarPasta);
            this.Controls.Add(this.txtOrigem);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelecionarPasta;
        private System.Windows.Forms.TextBox txtOrigem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox logSaida;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnRealizarOperacao;
        private System.Windows.Forms.Button btnSelecionarPastaLog;
        private System.Windows.Forms.TextBox txtDestinoLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelecionarPastaImagem;
        private System.Windows.Forms.TextBox txtDestinoImagem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIntervalo1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIntervalo2;
        private System.Windows.Forms.Label label7;
    }
}

