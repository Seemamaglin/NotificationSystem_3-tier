namespace NotificationSystem_3_tier.Presentation
{
    public static class InputHelper
    {
        public static int ReadPositiveInt(string num)
            {
                while(true)
                {
                    Console.Write(num);
                    if (int.TryParse(Console.ReadLine(), out int value) && value > 0) 
                        return value;
                    Console.WriteLine("Please enter a positive number");
                }
            }
            public static string ReadNonEmptyString(string msg)
            {
                while(true)
                {
                    Console.Write(msg);
                    string? message=Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(message))
                        return message.Trim();
                    Console.WriteLine("Message cannot be empty");
                }
            }

            public static string ReadValidEmail(string mail)
            {
                while (true)
                {
                    Console.Write(mail);
                    string? email=Console.ReadLine()?.Trim();

                    if(string.IsNullOrWhiteSpace(email))
                    {
                        Console.WriteLine("Email cannot be empty");
                        continue;
                    }

                    int atIndex=email.IndexOf('@');
                    int dotIndex=email.LastIndexOf('.');
                    
                    if (atIndex >0  && dotIndex >atIndex+1 && dotIndex<email.Length-1)
                    {
                        return email;
                    }
                    Console.WriteLine($"'{email}' is not a valid email. Example: name@gamil.com");
                    continue;
                }
            }

            public static string ReadValidPhone(string number)
            {
                while(true)
                {
                    Console.Write(number);
                    string? num=Console.ReadLine()?.Trim();

                    if(string.IsNullOrWhiteSpace(num))
                    {
                        Console.WriteLine("Phone number cannot be empty");
                        continue;
                    }

                    int digitCount=0;
                    bool allValid=true;

                    foreach (char c in num)
                    {
                        if (char.IsDigit(c))
                        {
                            digitCount++;
                            continue;
                        }
                        if (c=='+' || c=='-' || c==' ')
                            continue;
                        allValid=false;
                        break;
                    }

                    if (!allValid || digitCount < 10)
                    {
                        Console.WriteLine($"'{num}' is not valid. Use digits only (min 10 digits).");
                        continue;   
                    }

                    return num;
                }
            }
    }
}