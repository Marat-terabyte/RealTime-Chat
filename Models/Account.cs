using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models
{
    public class Account : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _secretWord;
        
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }
        public string? Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        public string? SecretWord
        {
            get
            {
                return _secretWord;
            }
            set
            {
                _secretWord = value;
                OnPropertyChanged("SecretWord");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}