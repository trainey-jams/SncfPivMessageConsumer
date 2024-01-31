namespace PIV_POC_Client.Models.PivMessage
{
    public class ListeCodes
    {
        public List<Valeur> valeur { get; set; } = new List<Valeur>();
        public string typeDefaut { get; set; } = string.Empty;
    }
}
