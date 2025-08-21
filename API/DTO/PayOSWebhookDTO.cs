namespace API.DTO
{
	public class PayOSWebhookDTO
	{
		public string orderCode { get; set; }
		public string status { get; set; }
		public string checksum { get; set; }
	}
}
