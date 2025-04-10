namespace POC_Leitura_Imagem
{
    public class ResultadoOCR
    {
        public string Numero { get; set; } = "";
        public string Erro { get; set; } = "";
        public bool Sucesso => !string.IsNullOrEmpty(Numero);
    }
}