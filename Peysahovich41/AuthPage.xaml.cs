using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Peysahovich41
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
        public partial class AuthPage : Page
        {
            private int ErrorExit = 0;
            private static Random _random = new Random();
            private string Captchafill;
            public AuthPage()
            {
                InitializeComponent();
                CaptchaGeneration();
                CaptchTBandTB.Visibility = Visibility.Hidden;
                Captcha.Visibility = Visibility.Hidden;
            }

            private async void LoginBtn_Click(object sender, RoutedEventArgs e)
            {
                string login = LoginTB.Text;
                string password = PasswordTB.Text;
                string captcha = CaptchaTB.Text;
                if (ErrorExit == 0)
                {
                    if (login == "" || password == "")
                    {
                        MessageBox.Show("Есть пустые поля");
                        return;
                    }
                }
                else
                {
                    if (login == "" || password == "" || captcha == "")
                    {
                        MessageBox.Show("Есть пустые поля");
                        return;
                    }
                }


                if (ErrorExit > 0)
                {
                    Captchafill = captchaOneWord.Text + captchaTwoWord.Text + captchaThreeWord.Text + captchaFourWord.Text;
                    if (captcha != Captchafill)
                    {
                        MessageBox.Show("Неверная капча");
                        CaptchaGeneration();
                        return;
                    }
                }
                User user = Peysahovich41Entitie.GetContext().User.ToList().Find(p => p.UserLogin == login && p.UserPassword == password);
                if (user != null)
                {
                    Manager.MainFrame.Navigate(new ProductPage(user));
                    LoginTB.Text = "";
                    PasswordTB.Text = "";
                    CaptchaTB.Text = "";
                    CaptchTBandTB.Visibility = Visibility.Hidden;
                    Captcha.Visibility = Visibility.Hidden;
                    ErrorExit = 0;
                }
                else
                {
                    MessageBox.Show("Неверные данные");
                    CaptchaGeneration();
                    ErrorExit++;
                    CaptchTBandTB.Visibility = Visibility.Visible;
                    Captcha.Visibility = Visibility.Visible;
                    LoginBtn.IsEnabled = false;
                    await Task.Delay(10000);
                    LoginBtn.IsEnabled = true;

                }

            }

            private void LoginForAnon_Click(object sender, RoutedEventArgs e)
            {
                Manager.MainFrame.Navigate(new ProductPage(null));
                LoginTB.Text = "";
                PasswordTB.Text = "";
                CaptchaTB.Text = "";
                CaptchTBandTB.Visibility = Visibility.Hidden;
                Captcha.Visibility = Visibility.Hidden;
                ErrorExit = 0;
            }

            private static char CharGeneration()
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                return chars[_random.Next(chars.Length)];
            }
            private void CaptchaGeneration()
            {
                captchaOneWord.Text = CharGeneration().ToString();
                captchaTwoWord.Text = CharGeneration().ToString();
                captchaThreeWord.Text = CharGeneration().ToString();
                captchaFourWord.Text = CharGeneration().ToString();
            }

        }
    }

