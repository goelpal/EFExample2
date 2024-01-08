
using EFExample2;

Console.WriteLine("Hello, World!");

MyDbContext Context = new MyDbContext();

showMainMenu();

//Function to show the main menu on the console
void showMainMenu()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Welcome to the Car application! Please select the relevant option.");
    Console.WriteLine("1-List All the Cars");
    Console.WriteLine("2-Add a new Car");
    Console.WriteLine("3-Edit a Car");
    Console.WriteLine("4-Delete a Car");
    Console.WriteLine("5-Exit the application");

    Console.Write("Enter your choice : ");
    Console.ResetColor();

    string userInput = Console.ReadLine();
    switch (userInput)
    {
        case "1":
            ListCars();//List all the Cars from the database
            showMainMenu();
            break;
        case "2":
            AddCar();//Add a new Car in the database
            break;
        case "3":
            EditCar();//Edit a Car in the database
            break;
        case "4":
            DeleteCar();//Delete a Car in the database
            break;
        case "5":
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Thank you for using this application!");//Quit the application
            Console.ResetColor();
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid Selection. Please try again.");//Invalid input from the user
            Console.ResetColor();
            showMainMenu();
            break;
    }
}

//Function to edit the Car details in the database
void EditCar()
{
    ListCars();
    int Flag = 0; //Variable to carry validations on data entered by the user

    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Enter the ID you want to edit :");
    string IdValue = Console.ReadLine();

    //Validations on the input value
    if (string.IsNullOrEmpty(IdValue))
    {
        Flag = 1;
    }

    bool ValidId = Int32.TryParse(IdValue, out int outId);
    if (Flag == 0 && ValidId)
    {
        List<Car> SelectedCar = Context.Cars.Where(x => x.Id == outId).ToList();
        if (SelectedCar.Count > 0)
        {
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("ID".PadRight(5) + "BRAND".PadRight(15) + "MODEL".PadRight(15) + "YEAR");

            foreach (Car item in SelectedCar)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(item.Id.ToString().PadRight(5) + item.Brand.ToUpper().PadRight(15) + item.Model.ToUpper().PadRight(15) + item.Year);
                Console.ResetColor();
            }
            Console.WriteLine("---------------------------------------------------------");

            var car1 = Context.Cars.FirstOrDefault(x => x.Id == outId);

            //Take user input for the new data

            Console.WriteLine("Old Brand : " + car1.Brand);
            Console.Write("New Brand : ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string NewBrand = Console.ReadLine();
            Console.ResetColor();

            Console.WriteLine("Old Model : " + car1.Model);
            Console.Write("New Model : ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string NewModel = Console.ReadLine();
            Console.ResetColor();

            Console.WriteLine("Old Year : " + car1.Year);
            Console.Write("New Year : ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string NewYear = Console.ReadLine();
            Console.ResetColor();

            bool ValidYear = Int32.TryParse(NewYear, out int outYear);

            car1.Brand = NewBrand;
            car1.Model = NewModel;
            car1.Year = outYear;

            /*

            if (string.IsNullOrEmpty(brand))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This is an invalid entry.");
                Console.ResetColor();
                flag = 1;
            }
            else if (brand.ToLower().Trim() == "q")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Exiting the Car Add application.");
                Console.ResetColor();
                break;
            }
            */


            Context.Cars.Update(car1);
            Context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Record successfully edited in the database.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Record with Id:" + outId + " doesnot exist.");
            Console.ResetColor();
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("This is an invalid entry. Please try again.");
        Console.ResetColor();
    }

    showMainMenu();
}

//Function to delete a car from the database
void DeleteCar()
{
    ListCars();
    int Flag = 0; //Variable to carry validations on data entered by the user

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Enter the ID you want to delete :");
        string IdValue = Console.ReadLine();

        //Validations on the input value
        if (string.IsNullOrEmpty(IdValue))
        {   
            Flag = 1;
        }

        bool ValidId = Int32.TryParse(IdValue, out int outId);
        if(Flag == 0 && ValidId)
        {
            List<Car> SelectedCar = Context.Cars.Where(x => x.Id == outId).ToList();
            if (SelectedCar.Count > 0)
            {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("ID".PadRight(5) + "BRAND".PadRight(15) + "MODEL".PadRight(15) + "YEAR");
       
                foreach (Car item in SelectedCar)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(item.Id.ToString().PadRight(5) + item.Brand.ToUpper().PadRight(15) + item.Model.ToUpper().PadRight(15) + item.Year);
                    Console.ResetColor();
                }
                Console.WriteLine("---------------------------------------------------------");

                var car1 = Context.Cars.FirstOrDefault(x => x.Id == outId);
                Context.Cars.Remove(car1);
                Context.SaveChanges();
                Console.WriteLine("Record successfully deleted from the database.");
            }
            else
            {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Record with Id:" + outId + " doesnot exist.");
                    Console.ResetColor();
            }    
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry. Please try again.");
            Console.ResetColor();
        }

    showMainMenu();
}

//Function to List all the Cars from the database
void ListCars()
{
    List<Car> Result = Context.Cars.ToList();
    if (Result.Count > 0)
    {
        Console.WriteLine("---------------------------------------------------------");
        Console.WriteLine("ID".PadRight(5) + "BRAND".PadRight(15) + "MODEL".PadRight(15) + "YEAR");
       
        foreach (Car item in Result)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(item.Id.ToString().PadRight(5) + item.Brand.ToUpper().PadRight(15) + item.Model.ToUpper().PadRight(15) + item.Year);
            Console.ResetColor();
        }
        Console.WriteLine("---------------------------------------------------------");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("There is no data to display.");
        Console.ResetColor();
    }
    
}

//Function to add a car in the database
void AddCar()
{
    while(true)
    {
        Car car = new Car();

        int flag = 0; // Flag to record any invalid entries
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("To enter a new Car details - follow the steps | To Quit Enter : 'Q'");
        Console.ResetColor();

        //Entering Car Brand info
        Console.Write("Enter the Brand of Car : ");
        string brand = Console.ReadLine();
        if (string.IsNullOrEmpty(brand))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry.");
            Console.ResetColor();
            flag = 1;
        }
        else if (brand.ToLower().Trim() == "q")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exiting the Car Add application.");
            Console.ResetColor();
            break;
        }

        //Entering Car Model info
        Console.Write("Enter the Model of Car : ");
        string model = Console.ReadLine();
        if (string.IsNullOrEmpty(model))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry.");
            Console.ResetColor();
            flag = 1;
        }
        else if (model.ToLower().Trim() == "q")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exiting the Car Add application.");
            Console.ResetColor();
            break;
        }

        //Entering Car Year info
        Console.Write("Enter the Year of Car : ");
        string year = Console.ReadLine();
        if (string.IsNullOrEmpty(year))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry.");
            Console.ResetColor();
            flag = 1;
        }
        else if (year.ToLower().Trim() == "q")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exiting the Car Add application.");
            Console.ResetColor();
            break;
        }
        bool isValidYear = Int32.TryParse(year, out int validyear);
        if (!isValidYear)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry.");
            Console.ResetColor();
            flag = 1;
        }

        if (flag == 1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There is an invalid entry. Please try again.");
            Console.ResetColor();
        }
        else
        {
            //Assigning values to the Car object
            car.Brand = brand;
            car.Model = model;
            car.Year = validyear;

            //Adding value to the database
            Context.Cars.Add(car);
            Context.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The Car details are successfully added to the database.");
            Console.ResetColor();
        }
    }
    showMainMenu();
}

