using System.Text.RegularExpressions;

namespace Lesson_12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string login = "login";
                string password = "password1";
                string comfirmPassword = "password1";
                if (Validation.Validate(login, password, comfirmPassword))
                {
                    Console.WriteLine("Валидация прошла успешно");
                }
            }
            catch (WrongLoginException ex)
            {
                Console.WriteLine($"{ex.Message}: ['{ex.Login}']");
            }
            catch (WrongPasswordException ex)
            {
                Console.WriteLine($"{ex.Message} ['{ex.Password}', '{ex.ConfirmPassword}']");
            }
        }
    }

    class Validation
    {
        public static bool Validate(string login, string password, string confirmPassword)
        {
            if (login.Contains(" "))
            {
                throw new WrongLoginException("Неверно введен логин: содержит пробел", login);
            }

            if (login.Length > 20)
            {
                throw new WrongLoginException("Неверно введен логин: длина больше 20", login);
            }

            if (password.Contains(" "))
            {
                throw new WrongPasswordException("Неверно введен пароль: содержит пробел", password, confirmPassword);
            }

            if (password.Length > 20)
            {
                throw new WrongPasswordException("Неверно введен пароль: длина больше 20", password, confirmPassword);
            }

            if (!Regex.IsMatch(password, @"[0-9]+"))
            {
                throw new WrongPasswordException("Неверно введен пароль: не содержит ни одной цифры", password, confirmPassword);
            }
            //string.Compare(password, confirmPassword) 
            if (password != confirmPassword)
            {
                throw new WrongPasswordException("Пароли не совпадают", password, confirmPassword);
            }

            return true;
        }
    }

    class WrongLoginException : Exception
    {
        public string Login { get; }

        public WrongLoginException(string message, string login)
            : base(message)
        {
            Login = login;
        }

    }

    class WrongPasswordException : Exception
    {
        public string Password { get; }
        public string ConfirmPassword { get; }

        public WrongPasswordException(string message, string password, string confirmPassword)
            : base(message)
        {
            Password = password;
            ConfirmPassword = confirmPassword;
        }

    }
}