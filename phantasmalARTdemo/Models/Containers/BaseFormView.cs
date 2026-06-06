namespace phantasmalARTdemo.Models.Containers
{
    public class BaseFormView<T>
    {
        public List<string>? Errors { get; set; }
        public required T Entity { get; set; }
    }
}
