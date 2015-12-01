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
    class Program
    {
        static void Main(string[] args)
        {
            //Create an instance of the UserInterface class
            UserInterface userInterface = new UserInterface();

            //Import the database and show import message (This is done on startup by default)
            BeverageACullenEntities beverageACullenEntities = new BeverageACullenEntities();
            userInterface.DisplayImportSuccess();

            //Display the Welcome Message to the user
            userInterface.DisplayWelcomeGreeting();

            //Display the Menu and get the response. Store the response in the choice integer
            //This is the 'primer' run of displaying and getting.
            int choice = userInterface.DisplayMenuAndGetResponse();

            while (choice != 6)
            {
                switch (choice)
                {
                    case 1:
                        
                        //Display entire list of beverages
                        userInterface.DisplayAllItems(beverageACullenEntities);
                        break;

                    case 2:
                        //Search the list for a beverage by ID
                        string searchQuery = userInterface.GetSearchQuery();

                        Beverage searchBeverage = beverageACullenEntities.Beverages.Find(searchQuery);

                        if (searchBeverage != null)
                        {
                            userInterface.DisplayItemFound();
                            userInterface.DisplayItem(searchBeverage);
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }
                        break;

                    case 3:
                        //Add a beverage item to the list

                        //Get values from user
                        string[] addItemInfo = userInterface.GetItemInformation(false,"");

                        //Make sure the user entered ID is not already on the list
                        if (beverageACullenEntities.Beverages.Find(addItemInfo[0]) == null)
                        {
                            //Create a new beverage and import values from user input
                            Beverage addBeverage = new Beverage();
                            addBeverage.id = addItemInfo[0];
                            addBeverage.name = addItemInfo[1];
                            addBeverage.pack = addItemInfo[2];
                            addBeverage.price = Convert.ToDecimal(addItemInfo[3]);
                            addBeverage.active = Convert.ToBoolean(addItemInfo[4]);

                            //Add the beverage to the database
                            beverageACullenEntities.Beverages.Add(addBeverage);
                            beverageACullenEntities.SaveChanges();
                            userInterface.DisplayAddItemSuccess();
                            userInterface.DisplayItem(addBeverage);

                        }
                        else
                        {
                            userInterface.DisplayItemAlreadyExistsError();
                        }
                        break;

                    case 4:
                        //Update a beverage item in the database

                        //Get search item ID from user
                        string updateQuery = userInterface.GetSearchQuery();

                        //Find the item by its ID
                        Beverage updateBeverage = beverageACullenEntities.Beverages.Find(updateQuery);

                        //If found...
                        if (updateBeverage != null)
                        {
                            //Display the item and directions for user reference
                            userInterface.DisplayItemFound();
                            userInterface.DisplayItem(updateBeverage);
                            userInterface.DisplayUpdateDirections();

                            //get updated values from user
                            string[] updateItemInfo = userInterface.GetItemInformation(true, updateBeverage.id);
                            updateBeverage.name = updateItemInfo[1];
                            updateBeverage.pack = updateItemInfo[2];
                            updateBeverage.price = Convert.ToDecimal(updateItemInfo[3]);
                            updateBeverage.active = Convert.ToBoolean(updateItemInfo[4]);

                            beverageACullenEntities.SaveChanges();

                            userInterface.DisplayUpdateSuccess();
                            userInterface.DisplayItem(updateBeverage);



                        }
                        else //if not found...
                        {
                            userInterface.DisplayItemFoundError();
                        }
                        break;

                    case 5:
                        //Delete beverage from database
                        string deleteQuery = userInterface.GetSearchQuery();
                        Beverage deleteBeverage = beverageACullenEntities.Beverages.Find(deleteQuery);

                        if (deleteBeverage != null)
                        {
                            userInterface.DisplayItemFound();
                            userInterface.DisplayItem(deleteBeverage);

                            beverageACullenEntities.Beverages.Remove(deleteBeverage);
                            beverageACullenEntities.SaveChanges();

                            userInterface.DisplayDeleteMessage();

                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }

                        break;
                }

                //Get the new choice of what to do from the user
                choice = userInterface.DisplayMenuAndGetResponse();
            }
        }
    }
}
