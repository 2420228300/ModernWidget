using CommunityToolkit.Mvvm.ComponentModel;

namespace ModernBox.Models
{
    public class WidgetsKind : ObservableRecipient
    {
        private String? categoryName;

        public String CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        public List<Widget> CategoryItems
        {
            get; set;
        }
    }
}