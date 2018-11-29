namespace Tailenders.Common
{
    public class EnumPickerOption
    {
        public EnumPickerOption(int value, string text)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public int Value { get; set; }
    }
}
