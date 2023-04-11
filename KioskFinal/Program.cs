//KIOSK PROJECT
//CLARA AND JOSEPH
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

DateTime today = DateTime.Now;

//MAIN
Till Kiosk;

//SET VALUE OF EACH BILL AND COIN ACCEPTED
Kiosk.hundredsValue = 100;
Kiosk.fiftiesValue = 50;
Kiosk.twentiesValue = 20;
Kiosk.tensValue = 10;
Kiosk.fivesValue = 5;
Kiosk.twosValue = 2;
Kiosk.onesValue = 1;
Kiosk.halfdollValue = 0.50m;
Kiosk.quartersValue = 0.25m;
Kiosk.dimesValue = 0.10m;
Kiosk.nickelsValue = 0.05m;
Kiosk.penniesValue = 0.01m;

//SET NUMBER OF BILLS AND COINS IN TILL
Kiosk.hundreds = 2;
Kiosk.fifties = 3;
Kiosk.twenties = 5;
Kiosk.tens = 5;
Kiosk.fives = 6;
Kiosk.twos = 1;
Kiosk.ones = 1;
Kiosk.halfdoll = 1;
Kiosk.quarters = 3;
Kiosk.dimes = 5;
Kiosk.nickels = 2;
Kiosk.pennies = 5;

//BOOL TO TEST IF USER WOULD LIKE TO PROCESS ANOTHER TRANSACTION 
bool runAgain = true;
decimal amtDue = 0;
string card = " ";
decimal changeDue = 0;
decimal cBack = 0;
string text = " ";

//CREATING USER FRIENDLY HEADER
Console.ForegroundColor = ConsoleColor.Yellow;
text = "WELCOME TO CO-Jo's SELF CHECKOUT KIOSK";
text = text.PadLeft(125, ' ');
Console.WriteLine(text);
text = "Press enter to start a transaction.";
text = text.PadLeft(124, ' ');
Console.WriteLine(text);

//INPUT VALIDATION FOR BEGINNING TRANSACTION
while (Console.ReadKey(true).Key != ConsoleKey.Enter)
{
    Console.Clear();
    text = "WELCOME TO CO-Jo's SELF CHECKOUT KIOSK";
    text = text.PadLeft(125, ' ');
    Console.WriteLine(text);
    text = "Press enter to start a transaction.";
    text = text.PadLeft(124, ' ');
    Console.WriteLine(text);
}
text = "#";
text = text.PadLeft(213, '#');
Console.WriteLine(text);

//GETTING ITEM TOTAL
amtDue = GetTotal();

//PROPMT USER FOR PAYMENT OPTION
Console.ForegroundColor = ConsoleColor.Yellow;
Console.BackgroundColor = ConsoleColor.Black;
Console.WriteLine("\nPress 1 for cash, 2 for credit: ");
ConsoleKeyInfo paymentOption = Console.ReadKey(true);

//INPUT VALIDATION FOR PAYMENT OPITON
while (paymentOption.KeyChar != '1' && paymentOption.KeyChar != '2')
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.WriteLine("\nPlease enter 1 or 2 to pay for your items: ");
    paymentOption = Console.ReadKey(true);
}

//IF STATEMENT TO DECIDE WHICH PAYMENT FUNCTION TO USE
if (paymentOption.KeyChar == '1')
{
    //CASH OPTION FUNCTION
    changeDue = payment1(ref Kiosk, amtDue, changeDue, card);
}
else if (paymentOption.KeyChar == '2')
{
    //CARD OPTION FUNCTION
    card = payment2(ref Kiosk, amtDue, changeDue, card, paymentOption);
}


//ASKING USER TO START ANOTHER TRANSACTION OR END HERE
Console.WriteLine("Would you like to complete another transaction? (y/n) : ");
runAgain = Console.ReadKey(true).KeyChar.ToString() == "y";

// INITIALIZE ARGUMENTS FOR TRANSACTION LOG
decimal pay = changeDue + amtDue;
string cashOut = pay.ToString();
string CardOut = cardReturn(card, amtDue);
string transNum = num();
string change = changeDue.ToString();

//LOGGING TRANSACTIONS
ProcessStartInfo startInfo = new ProcessStartInfo();
startInfo.FileName = "C:\\Users\\jorut\\source\\repos\\KioskTransactionLogging\\KioskTransactionLogging\\bin\\Debug\\net6.0\\KioskTransactionLogging.exe";
startInfo.Arguments = transNum + " " + cashOut + " " + CardOut + " " + change;
Process.Start(startInfo);

//IF STATEMENT BEGINNING ANOTHER TRANSACTION
if (runAgain == true)
{
    do
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        text = "WELCOME TO CO-Jo's SELF CHECKOUT KIOSK";
        text = text.PadLeft(125, ' ');
        Console.WriteLine(text);
        text = "Press enter to start a transaction.";
        text = text.PadLeft(124, ' ');
        Console.WriteLine(text);
        
        while (Console.ReadKey(true).Key != ConsoleKey.Enter)
        {
            Console.Clear();
            text = "WELCOME TO CO-Jo's SELF CHECKOUT KIOSK";
            text = text.PadLeft(125, ' ');
            Console.WriteLine(text);
            text = "Press enter to start a transaction.";
            text = text.PadLeft(124, ' ');
            Console.WriteLine(text);
        }
        text = "#";
        text = text.PadLeft(213, '#');
        Console.WriteLine(text);

        //GETTING ITEM TOTAL
        amtDue = GetTotal();

        //PROPMT USER FOR PAYMENT OPTION
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine("\nPress 1 for cash, 2 for credit: ");
        paymentOption = Console.ReadKey(true);

        //INPUT VALIDATION FOR PAYMENT OPITON
        while (paymentOption.KeyChar != '1' && paymentOption.KeyChar != '2')
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\nPlease enter 1 or 2 to pay for your items: ");
            paymentOption = Console.ReadKey(true);
        }

        //IF STATEMENT TO DECIDE WHICH PAYMENT FUNCTION TO USE
        if (paymentOption.KeyChar == '1')
        {
            //CASH OPTION FUNCTION
            changeDue = payment1(ref Kiosk, amtDue, changeDue, card);
        }
        else if (paymentOption.KeyChar == '2')
        {
            //CARD OPTION FUNCTION
            card = payment2(ref Kiosk, amtDue, changeDue, card, paymentOption);
        }
       

        //ASKING USER TO START ANOTHER TRANSACTION OR END HERE
        Console.WriteLine("Would you like to complete another transaction? (y/n) : ");
        runAgain = Console.ReadKey(true).KeyChar.ToString() == "y";
       
        Console.Clear();
    } while (runAgain);//end do loop
}
//END MAIN 

//PAYMENT FOR CASH FUNCTION
static decimal payment1(ref Till Kiosk, decimal amtDue, decimal changeDue, string card)
{
        //CASH OPTION FUNCTION
        changeDue = CashOption(ref Kiosk, amtDue);
    return changeDue;
}//END PAYMENT1

//PAYMENT FOR CASH FUNCTION
static string payment2(ref Till Kiosk, decimal amtDue, decimal changeDue, string card, ConsoleKeyInfo paymentOption)
{
    //CARD OPTION FUNCTION
    card = CardOption(ref Kiosk, amtDue, changeDue);

    return card;
}//END PAYMENT

//FUNCTIONS FOR CASH OPTION
static decimal CashOption(ref Till  Kiosk, decimal amtDue)
{
    //DECLARE AND INITIALIZE VARIABLES TO USE THROUGHOUT FUNCTION
    decimal userPayment = 0;
    decimal Item = 0;
    decimal cBack = 0;
    decimal changeDue = 0;

    //CALLING INPUT PAY FUNCTION
    changeDue = inputPay(amtDue, ref Kiosk);

    //TESTING VALIDATE TILL FUNCTION
    if (ValidateTill(changeDue, ref Kiosk) == true)
    {
        //CALLING DISPENSE CHANGE FUNCTION
        dispenseChange(changeDue, amtDue, ref Kiosk);
    }
    else // if kiosk is out of money
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("!ERROR!");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Kiosk cannot dispense change. **REFUND**");//check for Till prior to dispense change
        Console.WriteLine("Please select: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\t 1) use a card to complete the transaction");
        Console.WriteLine("\t 2) cancel the transaction");
        string Continue = Console.ReadLine();
        //INPUT VALIDATION FOR PAYMENT OPITON
        while (Continue != "1" && Continue != "2")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nPlease enter 1 or 2 to complete transaction. ");
            Continue = Console.ReadLine();
        }

        //IF STATEMENT TO DECIDE WHICH PAYMENT FUNCTION TO USE
        if (Continue == "1")
        {
            //CARD OPTION FUNCTION
            CardOption(ref Kiosk, amtDue, changeDue);
        }
        else if (Continue == "2")
        {
            Console.Clear();
            Console.WriteLine("Transaction Cancelled");
        }
    }

    Console.WriteLine("\nThanks for shopping with us today!");
    return changeDue;
}//end cash option

//GET TOTAL FUNCTION
static decimal GetTotal()
{
    //DECLARE AND INITIALIZE VARIABLES FOR THIS FUNCTION
    decimal amtDue = 0;
    decimal Item = 0;
   
    //PROMPT USER FOR INPUT AND GATHER USER INPUT
    //INPUT VALIDATION FOR ITEM PRICES
    Console.Write("Please enter item cost: $");
    while (!decimal.TryParse(Console.ReadLine(), out Item) || Item == 0 || Item < 0 || Item % 0.01m != 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Input not accepted. \nPlease enter a valid value: $");
        Console.BackgroundColor= ConsoleColor.Black;
        Console.ForegroundColor= ConsoleColor.Yellow;
    }
    //Simulate scanning of items
    Console.Beep();

    //TOTAL ITEM PRICE AND TEST FOR ANOTHER ITEM
    amtDue += Item;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Enter another? (y/n) : ");
    ConsoleKeyInfo getInput = Console.ReadKey(true);

    //INPUT VALIDATION FOR INPUTTING ANOTHER ITEM PRICE
    while (getInput.KeyChar != 'y' && getInput .KeyChar != 'n')
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid input. Please enter y or n.");
        getInput = Console.ReadKey(true);
    }
    if (getInput.KeyChar == 'y')
    {
        do
        {
            //PROMPT USER FOR INPUT AND GATHER USER INPUt
            //INPUT VALIDATION FOR ITEM PRICES
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Please enter item cost: $");
            while (!decimal.TryParse(Console.ReadLine(), out Item) || Item == 0 || Item < 0 || Item % 0.01m != 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Input not accepted. \nPlease enter a value greater than 0: $");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            //Simulate scanning of items
            Console.Beep();

            //TOTAL ITEM PRICE AND TEST FOR ANOTHER ITEM
            amtDue += Item;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter another? (y/n) : ");
            getInput = Console.ReadKey(true);
            while (getInput.KeyChar != 'y' && getInput.KeyChar != 'n')
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter y or n.");
                getInput = Console.ReadKey(true);
            }
        } while (getInput.KeyChar == 'y');    
    }
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.BackgroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine("\nTOTAL AMOUNT DUE: $" + amtDue);

    //GATHER USER INPUT AND COMPARE TO COUNT IN TILL while calculating amtdue
    return amtDue;
}//end getTotal

//INPUT PAY FUNCTION
static decimal inputPay(decimal amtDue, ref Till Kiosk)
{
    //DECLARE AND INITIALIZE VARIABLES FOR THIS FUNCTION
    decimal userPayment = 0;
    decimal changeDue = 0;

    //PROMPT USER FOR PAYMENT INPUT & INPUT VALIDATION
    Console.BackgroundColor = ConsoleColor.Black;
    Console.Write("Please enter a single bill or coin: $");
    while (!decimal.TryParse(Console.ReadLine(), out userPayment) || userPayment == 0 || userPayment < 0 
        || userPayment  %  0.01m == 0 && userPayment != Kiosk.hundredsValue && 
        userPayment != Kiosk.fiftiesValue && userPayment != Kiosk.twentiesValue && 
        userPayment != Kiosk.tensValue && userPayment != Kiosk.fivesValue && 
        userPayment != Kiosk.twosValue && userPayment != Kiosk.onesValue && 
        userPayment != Kiosk.halfdollValue && userPayment != Kiosk.quartersValue
      && userPayment != Kiosk.dimesValue && userPayment != Kiosk.nickelsValue
      && userPayment != Kiosk.penniesValue)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Input not accepted. \nPlease enter a value greater than 0: $");
        Console.ForegroundColor = ConsoleColor.White;
    }

    //UPDATING USER ON THE AMOUNT DUE 
    while (userPayment < amtDue)
    {
        //IF STATEMENTS THAT TEST EACH PAYMENT INPUT AND UPDATE AMOUNT DUE & TILL COUNT
        if (userPayment == 100)
        {
            Kiosk.hundreds += 1;
            amtDue = (amtDue - Kiosk.hundredsValue);
        }
        if (userPayment == 50)
        {
            Kiosk.fifties += 1;
            amtDue = (amtDue - Kiosk.fiftiesValue);
        }
        else if (userPayment == 20)
        {
            Kiosk.twenties += 1;
            amtDue = (amtDue - Kiosk.twentiesValue);
        }
        else if (userPayment == 10)
        {
            Kiosk.tens += 1;
            amtDue = (amtDue - Kiosk.tensValue);
        }
        else if (userPayment == 5)
        {
            Kiosk.fives += 1;
            amtDue = (amtDue - Kiosk.fivesValue);
        }
        else if (userPayment == 2)
        {
            Kiosk.twos += 1;
            amtDue = (amtDue - Kiosk.twosValue);
        }
        else if (userPayment == 1)
        {
            Kiosk.ones += 1;
            amtDue = (amtDue - Kiosk.onesValue);
        }
        else if (userPayment == 0.50m)
        {
            Kiosk.halfdoll += 1;
            amtDue = (amtDue - Kiosk.halfdollValue);
        }
        else if (userPayment == 0.25m)
        {
            Kiosk.quarters += 1;
            amtDue = (amtDue - Kiosk.quartersValue);
        }
        else if (userPayment == 0.10m)
        {
            Kiosk.dimes += 1;
            amtDue = (amtDue - Kiosk.dimesValue);
        }
        else if (userPayment == 0.05m)
        {
            Kiosk.nickels += 1;
            amtDue = (amtDue - Kiosk.nickelsValue);
        }
        else if (userPayment == 0.01m)
        {
            Kiosk.pennies += 1;
            amtDue = (amtDue - Kiosk.penniesValue);
        }

        //DISPLAY TO USER HOW MUCH IS DUE && PROMPT FOR NEXT INPUT
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("\nTOTAL AMOUNT DUE: $" + amtDue);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write("Please enter a single bill or coin: $");

        //INPUT VALIDATION FOR PAYMENT
        while (!decimal.TryParse(Console.ReadLine(), out userPayment) || userPayment == 0 || userPayment < 0
        || userPayment % 0.01m == 0 && userPayment != Kiosk.hundredsValue &&
        userPayment != Kiosk.fiftiesValue && userPayment != Kiosk.twentiesValue &&
        userPayment != Kiosk.tensValue && userPayment != Kiosk.fivesValue &&
        userPayment != Kiosk.twosValue && userPayment != Kiosk.onesValue &&
        userPayment != Kiosk.halfdollValue && userPayment != Kiosk.quartersValue
      && userPayment != Kiosk.dimesValue && userPayment != Kiosk.nickelsValue
      && userPayment != Kiosk.penniesValue)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Input not accepted. \nPlease enter a value greater than 0: $");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }//end while loop

    //IF STATEMENT FOR USER PAYMENT BEING GREATER THAN AMOUNT DUE
    if (userPayment >= amtDue)
    {
        if (userPayment == 100)
        {
            Kiosk.hundreds += 1;
        }
        if (userPayment == 50)
        {
            Kiosk.fifties += 1;
        }
        else if (userPayment == 20)
        {
            Kiosk.twenties += 1;
        }
        else if (userPayment == 10)
        {
            Kiosk.tens += 1;
        }
        else if (userPayment == 5)
        {
            Kiosk.fives += 1;
        }
        else if (userPayment == 2)
        {
            Kiosk.twos += 1;
        }
        else if (userPayment == 1)
        {
            Kiosk.ones += 1;
        }
        else if (userPayment == 0.50m)
        {
            Kiosk.halfdoll += 1;
        }
        else if (userPayment == 0.25m)
        {
            Kiosk.quarters += 1;
        }
        else if (userPayment == 0.10m)
        {
            Kiosk.dimes += 1;
        }
        else if (userPayment == 0.05m)
        {
            Kiosk.nickels += 1;
        }
        else if (userPayment == 0.01m)
        {
            Kiosk.pennies += 1;
        }
        //CALCULATE AND DISPLAY CHANGE DUE
        changeDue = userPayment - amtDue;
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("\nCHANGE DUE: $" + changeDue);

    }//end if statement
    return changeDue;
}//end inputPay

//Validate Till function; validates that we have enough money prior to dispensing change
static bool ValidateTill (decimal changeDue,  ref Till Kiosk)
{
    //DECLARE AND INITIALIZE VARIABLES
    bool change = false;
    decimal KioskSum = 0;
    decimal checkChange = changeDue;
    decimal hundred = Kiosk.hundreds * Kiosk.hundredsValue;
    decimal fifty = Kiosk.fifties * Kiosk.fiftiesValue;
    decimal twenty = Kiosk.twenties * Kiosk.twentiesValue;
    decimal ten = Kiosk.tens * Kiosk.tensValue;
    decimal five = Kiosk.fives * Kiosk.fivesValue;
    decimal two = Kiosk.twos * Kiosk.twosValue;
    decimal one = Kiosk.ones * Kiosk.onesValue;
    decimal halfdoll = Kiosk.halfdoll * Kiosk.halfdollValue;
    decimal quarter = Kiosk.quarters * Kiosk.quartersValue;
    decimal dime = Kiosk.dimes * Kiosk.dimesValue;
    decimal nickel = Kiosk.nickels * Kiosk.nickelsValue;
    decimal penny = Kiosk.pennies * Kiosk.penniesValue;

    KioskSum = hundred + fifty + twenty + ten + five + two + one + halfdoll + quarter + dime + nickel + penny;

    //CHECK TO VALIDATE THAT WE HAVE ENOUGH MONEY TO DISPENSE CHANGE
    if (changeDue > KioskSum)
    {
        change = false;
    }

    //CHECKS EACH AMOUNT USING REVERSE GREEDY ALGORITHM; UPDATES AS IT READS TRUE
    if(checkChange > 0)
    {
        if (changeDue >= 0.01m)
        {
            checkChange -= penny;
        }
        if (changeDue >= 0.05m)
        {
            checkChange -= nickel;
        }
        if (changeDue >= 0.10m)
        {
            checkChange -= dime;
        }
        if (changeDue >= 0.25m)
        {
            checkChange -= quarter;
        }
        if (changeDue >= 0.50m)
        {
            checkChange -= halfdoll;
        }
        if (changeDue >= 1)
        {
            checkChange -= one;
        }
        if (changeDue >= 2)
        {
            checkChange -= two;
        }
        if (changeDue >= 5)
        {
            checkChange -= five;
        }
        if (changeDue >= 10)
        {
            checkChange -= ten;
        }
        if (changeDue >= 20)
        {
            checkChange -= twenty;
        }
        if (changeDue >= 50)
        {
            checkChange -= fifty;
        }
        if (changeDue >= 100)
        {
            checkChange -= hundred;
        }
    }

    checkChange += changeDue;

    //DETERMINES WHAT BOOL TO SEND BACK
    if (checkChange > changeDue)
    {
        change = false;
    }
    else
    {
        change = true;
    }
    return change;
}//END VALIDATE TILL

//DISPENSE CHANGE FUNCTION
static void dispenseChange(decimal changeDue, decimal amtDue, ref Till Kiosk)
{
    //WHILE LOOP TO UPDATE TILL COUNT AND DISPLAY TO USER THE CHANGE THEY WILL RECEIVE
    while (changeDue > 0)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        if (changeDue >= 100 && Kiosk.hundreds > 0)
        {
            Kiosk.hundreds--;
            changeDue = changeDue - Kiosk.hundredsValue;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dispensing $100 bill");
            Console.Beep();
        }
        else if (changeDue >= 50 && Kiosk.fifties > 0)
        {
            Kiosk.fifties--;
            changeDue = changeDue - Kiosk.fiftiesValue;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dispensing $50 bill");
            Console.Beep();
        }
        else if (changeDue >= 20 && Kiosk.twenties > 0)
        {
            Kiosk.twenties--;
            changeDue = changeDue - Kiosk.twentiesValue;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dispensing $20 bill");
            Console.Beep();
        }
        else if (changeDue >= 10 && Kiosk.tens > 0)
        {
            Kiosk.tens--;
            changeDue = changeDue - Kiosk.tensValue;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dispensing $10 bill");
            Console.Beep();
        }
        else if (changeDue >= 5 && Kiosk.fives > 0)
        {
            Kiosk.fives--;
            changeDue = changeDue - Kiosk.fivesValue; 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dispensing $5 bill");
            Console.Beep();
        }
        else if (changeDue >= 2 && Kiosk.twos > 0)
        {
            Kiosk.twos--;
            changeDue = changeDue - Kiosk.twosValue; 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dispensing $2 bill");
            Console.Beep();
        }
        else if (changeDue >= 1 && Kiosk.ones > 0)
        {
            Kiosk.ones--;
            changeDue = changeDue - Kiosk.onesValue; 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dispensing $1 bill");
            Console.Beep();
        }
        else if (changeDue >= 0.50m && Kiosk.halfdoll > 0)
        {
            Kiosk.halfdoll--;
            changeDue = changeDue - Kiosk.halfdollValue;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dispensing $0.50 coin");
            Console.Beep();
        }
        else if (changeDue >= 0.25m && Kiosk.quarters > 0)
        {
            Kiosk.quarters--;   
            changeDue = changeDue - Kiosk.quartersValue; 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dispensing $0.25 coin");
            Console.Beep();
        }
        else if (changeDue >= 0.10m && Kiosk.dimes > 0)
        {
            Kiosk.dimes--;
            changeDue = changeDue - Kiosk.dimesValue; 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dispensing $0.10 coin");
            Console.Beep();
        }
        else if (changeDue >= 0.05m && Kiosk.nickels > 0)
        {
            Kiosk.nickels--;
            changeDue = changeDue - Kiosk.nickelsValue; 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dispensing $0.05 coin");
            Console.Beep();
        }
        else if (changeDue >= 0.01m && Kiosk.pennies > 0)
        {
            Kiosk.pennies--;
            changeDue = changeDue - Kiosk.penniesValue; 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dispensing $0.01 coin");
            Console.Beep();
        }
    }
    //end while loop
}//end dispenseChange

//FUNCTIONS FOR CARD OPTION
static string CardOption(ref Till Kiosk, decimal amtDue, decimal changeDue)
{
    //DECLARE AND INITIALIZE VARIABLES
    Random rnd = new Random();
    int i = 0;
    decimal cBack = 0;
    string card = " ";

    // CASH BACK FEATURE 
    Console.WriteLine("\nWould you like cash back? (y/n) ");
    ConsoleKeyInfo userInput = Console.ReadKey(true);

    //INPUT VALIDATION FOR CASHBACK
    while (userInput.KeyChar != 'n' && userInput.KeyChar != 'y')
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid input. Please enter y or n.");
        userInput = Console.ReadKey(true);
    }

    if (userInput.KeyChar == 'y')
    {
        Console.ForegroundColor= ConsoleColor.White;
        Console.Write("Enter an amount: $");
        
        //INPUT VALIDATION FOR CASHBACK AMOUNT
        while (!decimal.TryParse(Console.ReadLine(), out cBack) || cBack == 0 || cBack < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid Amount entered. Please enter an amount greater than 0: $");
        }

        //UPDATE AND DISPLAY NEW TOTAL AMOUNT TO REFLECT CASHBACK BEING ADDED
        amtDue = amtDue + cBack;
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("\nTOTAL AMOUNT DUE: $" + amtDue);

        // CREDIT CARD INPUT 
        Console.Write("Please enter your valid card number: ");
        card = Console.ReadLine();

        //FOR LOOP  TESTS THAT CARD INPUT IS ONLY NUMBERS
        for (i = 0; i < card.Length; i++)
        {
            //TESTS FOR LETTERS
            if (Char.IsLetter(card[i]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid card number. Please enter another card number: ");
                card = Console.ReadLine();
            }
            //TESTS FOR SYMBOLS
            else if (!Char.IsLetterOrDigit(card[i]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid card number. Please enter another card number: ");
                card = Console.ReadLine();
            }
            //TESTS THE LENGTH
            while (card.Length != 16)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid card number. Please enter another card number that contains 16 digits: ");
                card = Console.ReadLine();
            }
        }
        Validation(card);
    }
    else if (userInput.KeyChar == 'n')
    {
        Console.ForegroundColor= ConsoleColor.White;
        Console.Write("Please enter your valid card number: ");
        card = Console.ReadLine();
        for (i = 0; i < card.Length; i++)
        {

            if (Char.IsLetter(card[i]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid card number. Please enter another card number: ");
                card = Console.ReadLine();
            }
            else if (!Char.IsLetterOrDigit(card[i]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid card number. Please enter another card number: ");
                card = Console.ReadLine();
            }
            while (card.Length != 16)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid card number. Please enter another card number: ");
                card = Console.ReadLine();
            }
        }
        Validation(card);
    }
    //IDENTIFY CARD MERCHANT
    Console.BackgroundColor = ConsoleColor.Black;
    if (card.StartsWith("3"))
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Card Number: " + card + "\tMerchant: American Express");
        DisplayCard(ref Kiosk, card, amtDue, i, cBack,changeDue);
    }
    else if (card.StartsWith("4"))
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Card Number: " + card + "\tMerchant: Visa");
        DisplayCard(ref Kiosk, card, amtDue, i, cBack, changeDue);
    }
    else if (card.StartsWith("5"))
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Card Number: " + card + "\tMerchant: MasterCard");
        DisplayCard(ref Kiosk, card, amtDue, i, cBack, changeDue);
    }
    else if (card.StartsWith("6"))
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Card Number: " + card + "\tMerchant: Discover");
        DisplayCard(ref Kiosk, card, amtDue, i, cBack, changeDue);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Card Invalid");
        //PROPMT USER FOR PAYMENT OPTION
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("\nPress 1 for cash, 2 for credit: \n");
        ConsoleKeyInfo paymentOption = Console.ReadKey(true);
        //INPUT VALIDATION FOR PAYMENT OPITON
        while (paymentOption.KeyChar != '1' && paymentOption.KeyChar != '2')
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\nPlease enter 1 or 2 to pay for your items: ");
            paymentOption = Console.ReadKey(true);
        }

        //IF STATEMENT TO DECIDE WHICH PAYMENT FUNCTION TO USE
        if (paymentOption.KeyChar == '1')
        {
            //CASH OPTION FUNCTION
            amtDue = amtDue - cBack;
            CashOption(ref Kiosk, amtDue);
        }
        else if (paymentOption.KeyChar == '2')
        {
            //CARD OPTION FUNCTION
            CardOption(ref Kiosk, amtDue, changeDue);
            DisplayCard(ref Kiosk, card, amtDue, i, cBack, changeDue);
        }
    }

    //VALIDATION FUNCTION
    static int[] Validation(string a)
    {
        bool result = true;
        int[] cardNum = new int[16];
        int sum = 0;
        int paymentOption = 0;
        for (int g = 0; g < a.Length; g++)
        {
            char[] cardSpot = a.ToCharArray();
            for (int h = 0; h < cardSpot.Length; h++)
            {
                cardNum[h] = Convert.ToInt32(cardSpot[h].ToString());
            }
            //Luhn alogorithm
            for (int i = cardNum.Length - 2; i >= 0; i--) // for loop to compare 
            {
                //checking every other spot
                if (i % 2 == 0)
                {
                    cardNum[i] = cardNum[i] * 2;
                    //replacing double with single digit result
                    if (cardNum[i] > 9)
                    {
                        cardNum[i] = cardNum[i] - 9;
                    }
                    //getting the total
                    sum += cardNum[i];
                }
                else
                {
                    sum += cardNum[i];
                }
                //testing sum with check digit
                sum = sum + cardNum[15];
            }
            if (sum % 10 == 0)
            {
                result = true;
                return cardNum;
            }
            else
            {
                result = false;
            }
        }
        return cardNum;
    }//END OF VALIDATION FUNCTION

    //DISPLAY CARD FUNCTION
    static void DisplayCard(ref Till Kiosk, string card, decimal amtDue, int i, decimal cBack, decimal changeDue)//RUNS MONEY REQUEST && DISPLAY TO USER
    {
        decimal amtOwed = 0;

        //CALL MONEY REQUEST FUNCTION
        string[] displayCard = MoneyRequest(card, amtDue);

        //IF CARD RETURNS DECLINED
        if (displayCard[1] == "declined")
        {
            amtDue = amtDue - cBack;
            Console.Write(displayCard[0] + ":\t$" + displayCard[1]);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\tTransaction unsuccessful. ");
            Console.WriteLine("ERROR! Insufficient funds.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPlease use another form of payment. Press 1 for cash, 2 for credit: ");
            ConsoleKeyInfo paymentOption = Console.ReadKey(true);
            //INPUT VALIDATION FOR PAYMENT OPITON
            while (paymentOption.KeyChar != '1' && paymentOption.KeyChar != '2')
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("\nPlease enter 1 or 2 to pay for your items: ");
                paymentOption = Console.ReadKey(true);
            }

            //IF STATEMENT TO DECIDE WHICH PAYMENT FUNCTION TO USE
            if (paymentOption.KeyChar == '1')
            {
                //CASH OPTION FUNCTION
                CashOption(ref Kiosk, amtDue);
            }
            else if (paymentOption.KeyChar == '2')
            {
                //CARD OPTION FUNCTION
                CardOption(ref Kiosk, amtDue, changeDue);
            }
        }

        //CONVERTING THE STRING TO DECIMAL IF A VALUE IS RETURNED
        amtOwed = decimal.Parse(displayCard[1]);

        
        if (amtOwed != amtDue) // else if amtApproved < amtDue but > 0
        {
            amtDue = amtDue - amtOwed;
            Console.Write(displayCard[0] + "\tAmount Paid: $" + displayCard[1]);
            Console.Write("\nAmount due: ${0}", amtDue); // calculating the amount left to pay
            Console.WriteLine("\nPlease use another form of payment to complete transaction. Press 1 for cash, 2 for credit: ");
            ConsoleKeyInfo paymentOption = Console.ReadKey(true);

            //INPUT VALIDATION FOR PAYMENT OPITON
            while (paymentOption.KeyChar != '1' && paymentOption.KeyChar != '2')
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("\nPlease enter 1 or 2 to pay for your items: ");
                paymentOption = Console.ReadKey(true);
            }

            //IF STATEMENT TO DECIDE WHICH PAYMENT FUNCTION TO USE
            if (paymentOption.KeyChar == '1')
            {
                //CASH OPTION FUNCTION
                CashOption(ref Kiosk, amtDue);
            }
            else if (paymentOption.KeyChar == '2')
            {
                CardOption(ref Kiosk, amtDue, changeDue);
                //Validation FUNCTION
                Validation(card);
                MoneyRequest(card, amtDue);
            }
        }

        //IF CASHBACK AND/OR PAYMENT COVERED IN FULL 
        if (amtOwed >= amtDue && cBack > 0)
        {
            if (ValidateTill(cBack, ref Kiosk) == true)
            {
                Console.WriteLine("Approved. Cashback is being dispensed below.");
                dispenseChange(changeDue, amtDue, ref Kiosk);
            }
            else // if kiosk is out of money
            {
                int Continue = 0;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("!ERROR!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Kiosk cannot dispense change. **REFUND**");//check for Till prior to dispense change
                Console.WriteLine("Please select: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t 1) use a card to complete the transaction");
                Console.WriteLine("\t 2) cancel the transaction");

                //INPUT VALIDATION FOR PAYMENT OPITON
                while (Continue != 1 && Continue != 2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\nPlease enter 1 or 2 to complete transaction. ");
                    Continue = int.Parse(Console.ReadLine());
                }

                //IF STATEMENT TO DECIDE WHICH PAYMENT FUNCTION TO USE
                if (Continue == 1)
                {
                    //CARD OPTION FUNCTION
                    CardOption(ref Kiosk, amtDue, changeDue);
                }
                else if (Continue == 2)
                {
                    Console.Clear();
                }
            }
            amtOwed -= amtDue;
        }
        else if (amtOwed >= amtDue)
        {
            Console.Write("Approved. ");
        }
    }
    return card;
}

//MONEY REQUEST FUNCTION
static string[] MoneyRequest(string account_number, decimal amount)
{
    Random rnd = new Random();
    //50% CHANCE TRANSACTION PASSES OR FAILS
    bool pass = rnd.Next(100) < 50;
    //50% CHANCE THAT A FAILED TRANSACTION IS DECLINED
    bool declined = rnd.Next(100) < 50;
    if (pass)
    {
        return new string[] { account_number, amount.ToString() };
    }
    else
    {
        if (!declined)
        {
            return new string[] { account_number, (amount / rnd.Next(2, 6)).ToString() };
        }
        else
        {
            return new string[] { account_number, "declined" };
        }//end if
    }//end if
}//end if


// REASSIGN ARGUMENTS FOR TRANS LOG IF (RUN AGAIN) IS FALSE
pay = changeDue + amtDue;
cashOut =  pay.ToString();
CardOut = cardReturn(card, pay);
transNum = num();
change = changeDue.ToString();

//CARD RETURN FUNCTION
static string cardReturn(string card, decimal amtDue)
{
    string cardOut = " ";
    if (card.StartsWith("3"))
    {
        cardOut = card + "\tMerchant: American Express; Amount Paid: $" + amtDue.ToString();
    }
    else if (card.StartsWith("4"))
    {
        cardOut = card + "\tMerchant: Visa; Amount Paid: $" + amtDue.ToString();
    }
    else if (card.StartsWith("5"))
    {
        cardOut = card + "\tMerchant: MasterCard; Amount Paid: $" + amtDue.ToString();
    }
    else if (card.StartsWith("6"))
    {
        cardOut = card + "\tMerchant: Discover; Amount Paid: $" + amtDue.ToString();
    }
    else
    {
        cardOut = "N\\A";
    }
    return cardOut;
}

//NUM FUNCTION FOR TRANSACTION NUMBER
static string num()
{
    string transNum = "";
    Random random = new Random();
    for (int i = 0; i < 10; i++)
    {
        transNum += random.Next(1, 10);
    }
    return transNum;
}

//SENDING ARGUMENTS TO ANOTHER PROGRAM
startInfo = new ProcessStartInfo();
startInfo.FileName = "C:\\Users\\jorut\\source\\repos\\KioskTransactionLogging\\KioskTransactionLogging\\bin\\Debug\\net6.0\\KioskTransactionLogging.exe";
startInfo.Arguments = transNum + " " + cashOut + " " + CardOut + " " + change;
Process.Start(startInfo);

//STRUCTURE FOR THE TILL
public struct Till
{
    public decimal hundreds;
    public decimal fifties;
    public decimal twenties;
    public decimal tens;
    public decimal fives;
    public decimal twos;
    public decimal ones;
    public decimal hundredsValue;
    public decimal fiftiesValue;
    public decimal twentiesValue;
    public decimal tensValue;
    public decimal fivesValue;
    public decimal twosValue;
    public decimal onesValue;
    public decimal halfdoll;
    public decimal quarters;
    public decimal dimes;
    public decimal nickels;
    public decimal pennies;
    public decimal halfdollValue;
    public decimal quartersValue;
    public decimal dimesValue;
    public decimal nickelsValue;
    public decimal penniesValue;
}//end structure
