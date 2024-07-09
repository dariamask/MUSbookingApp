namespace BAL.Dto
{
    public class CreateResponse
    {
        public int TotalOrderlineRequestCount { get; set; }
        public int SuccessfulOrderlines { get; set; }
        public string? SuccesMessage { get; set; }
        public List<string>? Errors { get; set; }

        private string GetSuccessMessage(int successfulOrderlines, int totalOrderlineRequestCount) =>
            $"Привет любимый покупатель, " +
                $"успешно добавлено в корзину {successfulOrderlines} товаров из {totalOrderlineRequestCount}";

        public string GetResponseMessage()
        {
            SuccesMessage = GetSuccessMessage(SuccessfulOrderlines, TotalOrderlineRequestCount);
            
            return Errors is null
                ? SuccesMessage
                : SuccesMessage + ":" + Environment.NewLine + string.Join(Environment.NewLine, Errors);
              //: SuccesMessage + Environment.NewLine + Errors.Aggregate((current, next) => current + Environment.NewLine + next);
        }  
    }
}