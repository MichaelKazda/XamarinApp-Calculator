using DynamicExpresso;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CalculatorApp {
    public class MainPageViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        string input;

        public MainPageViewModel() {

            //Adds input key into the whole input
            addNum = new Command<string>((inputKey) => {
                Input += inputKey;
            });

            //Deletes nums in input
            del = new Command((delType) => {
                if (delType == "one" && Input.Length > 0) {
                    Input = Input.Substring(0, Input.Length - 1);
                } else if (delType == "all") {
                    Input = string.Empty;
                }
            });

            result = new Command(() => {
                try {
                    var interpreter = new Interpreter();
                    Input = interpreter.Eval(Input).ToString();
                } catch {
                    Input = "SYNTAX ERROR";
                }
            });
        }

        public string Input {
            set { 
                if(input != value) {
                    input = value;
                    OnPropertyChanged("Input");
                }
            }
            get { return input; }
        }

        public Command addNum { set; get; }

        public Command del { set; get; }

        public Command result { set; get; }


        /**
         * Hides keyboard
         */
        public interface IKeyboardHelper {
            void HideKeyboard();
        }
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
