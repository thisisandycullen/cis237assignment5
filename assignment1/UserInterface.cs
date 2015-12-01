//Update by: Andy Cullen
//Original Author: David Barnes
//CIS 237
//Assignment 1 >>> Assignment 5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    class UserInterface
    {
        const int maxMenuChoice = 6;

        //---------------------------------------------------
        //Public Methods
        //---------------------------------------------------


        //Display Import Success
        public void DisplayImportSuccess()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("The beverage list has been imported successfully.");
            Console.WriteLine();
            Console.ResetColor();
        }

        //Display Welcome Greeting
        public void DisplayWelcomeGreeting()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("  Welcome to the Beverage List Manager!  ");
            Console.WriteLine("-----------------------------------------");
        }

        //Display Menu And Get Response
        public int DisplayMenuAndGetResponse()
        {
            //declare variable to hold the selection
            string selection;

            //Display menu, and prompt
            this.DisplayMenu();
            this.DisplayPrompt();

            //Get the selection they enter
            selection = this.getSelection();

            //While the response is not valid
            while (!this.verifySelectionIsValid(selection))
            {
                //display error message
                this.DisplayErrorMessage();

                //display the prompt again
                this.DisplayPrompt();

                //get the selection again
                selection = this.getSelection();
            }
            //Return the selection casted to an integer
            return Int32.Parse(selection);
        }

        //Get the search query from the user
        public string GetSearchQuery()
        {
            Console.WriteLine();
            Console.WriteLine("Enter the ID of the beverage you are searching for:");
            Console.Write("> ");
            return Console.ReadLine();
        }

        //Get new item information from the user
        public string[] GetItemInformation(bool updatingItem, string itemID)
        {
            string id = itemID; //this will be passed in as a blank string or an existing ID

            //Skip this when updating an item
            if (!updatingItem)
            {
                Console.WriteLine();
                Console.WriteLine("What is the item's ID?");
                Console.Write("> ");
                id = Console.ReadLine();
            }
            
            //Get data for remaining fields
            Console.WriteLine("What is the item's Description?");
            Console.Write("> ");
            string description = Console.ReadLine();
            Console.WriteLine("What is the item's Pack?");
            Console.Write("> ");
            string pack = Console.ReadLine();

            //Update: added price and active status
            Console.WriteLine("What is the item's Price?");
            Console.Write("> ");
            string price = Console.ReadLine();

            Console.WriteLine("Is the item Active? Y or N ");
            Console.Write("> ");
            string activeStatus = Console.ReadLine();

            if (activeStatus.ToUpper() == "Y")
                activeStatus = "true";
            else
                activeStatus = "false";

            return new string[] { id, description, pack, price, activeStatus };
        }

        //Display All Items
        public void DisplayAllItems(BeverageACullenEntities beverageACullenEntities)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();

            foreach (Beverage displayBeverage in beverageACullenEntities.Beverages)
            {
                string activeString;

                if (displayBeverage.active)
                {
                    activeString = "Active";
                }
                else
                {
                    activeString = "Not active";
                }

                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine(displayBeverage.id.Trim() + ": " + displayBeverage.name.Trim() + Environment.NewLine +
                                    displayBeverage.pack.Trim().PadRight(39) + displayBeverage.price.ToString("c").PadRight(11) +
                                    activeString);
                Console.WriteLine("------------------------------------------------------------");
            }

            Console.ResetColor();
        }

        //Display All Items Error
        public void DisplayEmptyListError()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine();
            Console.WriteLine("Error: The list is empty.");
            Console.ResetColor();
        }

        //Display item deleted
        public void DisplayDeleteMessage()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.WriteLine("The item above was deleted from the list.");
            Console.ResetColor();
        }

        //Display Item Found
        public void DisplayItemFound()
        {
            
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.WriteLine("Item found!");
            Console.ResetColor();
        }

        //Display Item Found
        public void DisplayItem(Beverage foundBeverage)
        {
            string activeString;

            if (foundBeverage.active)
            {
                activeString = "Active";
            }
            else
            {
                activeString = "Not active";
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine(foundBeverage.id.Trim() + ": " + foundBeverage.name.Trim() + Environment.NewLine +
                                foundBeverage.pack.Trim().PadRight(39) + foundBeverage.price.ToString("c").PadRight(11) +
                                activeString);
            Console.WriteLine("------------------------------------------------------------");
            Console.ResetColor();
        }

        //Display Item Found Error
        public void DisplayItemFoundError()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine();
            Console.WriteLine("A match was not found.");
            Console.ResetColor();
        }

        //Display update directions
        public void DisplayUpdateDirections()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("UPDATE ITEM: Please enter updated values for this item:");
            Console.ResetColor();
        }

        //Display Added Wine Item
        public void DisplayAddItemSuccess()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.WriteLine("The item was added successfully!");
            Console.ResetColor();
        }

        //Display update item success
        public void DisplayUpdateSuccess()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.WriteLine("The item was updated successfully!");
            Console.ResetColor();
        }

        //Display Item Already Exists Error
        public void DisplayItemAlreadyExistsError()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine();
            Console.WriteLine("Error: An item with that ID already exists. The item was not added.");
            Console.ResetColor();
        }

        //---------------------------------------------------
        //Private Methods
        //---------------------------------------------------

        //Display the Menu
        private void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1. List all beverages");
            Console.WriteLine("2. Search for a beverage");
            Console.WriteLine("3. Add a new beverage to the list");
            Console.WriteLine("4. Update a beverage");
            Console.WriteLine("5. Delete a beverage from the list");
            Console.WriteLine("6. Close the program");
        }

        //Display the Prompt
        private void DisplayPrompt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.Write("Enter Your Choice: ");
            Console.ResetColor();
        }

        //Display the Error Message
        private void DisplayErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine();
            Console.WriteLine("Invalid input. Please try again.");
            Console.ResetColor();
        }

        //Get the selection from the user
        private string getSelection()
        {
            return Console.ReadLine();
        }

        //Verify that a selection from the main menu is valid
        private bool verifySelectionIsValid(string selection)
        {
            //Declare a returnValue and set it to false
            bool returnValue = false;

            try
            {
                //Parse the selection into a choice variable
                int choice = Int32.Parse(selection);

                //If the choice is between 0 and the maxMenuChoice
                if (choice > 0 && choice <= maxMenuChoice)
                {
                    //set the return value to true
                    returnValue = true;
                }
            }
            //If the selection is not a valid number, this exception will be thrown
            catch (Exception e)
            {
                //set return value to false even though it should already be false
                returnValue = false;
            }

            //Return the reutrnValue
            return returnValue;
        }
    }
}
