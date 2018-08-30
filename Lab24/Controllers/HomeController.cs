using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab24.Models;

namespace Lab24.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() //create
        {
            //READ part of the CRUD opperation
            gccoffeeshopEntities ORM = new gccoffeeshopEntities();
            ViewBag.items = ORM.items.ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NewItem() //post
        {
       
            return View();
        }

        public ActionResult SaveNewItem(item newItem) //add
        {
            gccoffeeshopEntities ORM = new gccoffeeshopEntities(); //ORM... needed for anytime you talk to db (found in index)
            // ToDo: data validation!!! if statement
            ORM.items.Add(newItem); //adding newItem obj to the form serverside
            ORM.SaveChanges(); //sync with the database and show the changes and what is saved

            return RedirectToAction("Index"); //bounce back to index to show the list of items we have
        }

        public ActionResult DeleteItem(int itemid) //update
        {
            gccoffeeshopEntities ORM = new gccoffeeshopEntities();
            //for loop to find the id
            //the Find method is used to find obj by using the primary key
            item ItemToDelete = ORM.items.Find(itemid); //create new item to find the item you want to delete based on the primary key
            //remove
            ORM.items.Remove(ItemToDelete); //remove that obj -- ItemToDelete
            //save changes on db
            ORM.SaveChanges(); //ToDo: use try and catch to give an exception

            return RedirectToAction("Index"); //return to view with the item list and removed
        }

        public ActionResult ItemDetails(int itemid) //this action will show the data before the update
        {   //this action will show the data before the update
            gccoffeeshopEntities ORM = new gccoffeeshopEntities();

            //find the item
            item ItemToEdit = ORM.items.Find(itemid);

            //sent it back to view
            ViewBag.ItemToEdit = ItemToEdit;
            return View();
        }

        public ActionResult SaveChanges(item updatedItem) //update
        {   
            gccoffeeshopEntities ORM = new gccoffeeshopEntities();

            //find the old record that you're updating
            item OldRecord = ORM.items.Find(updatedItem.itemid);
            //ToDo: check for null!!!!!!
            //if (OldRecord == null)
            //{
            //    return View("Index");
            //}
            
            //don't need to update itemid because that stays the same
            OldRecord.name = updatedItem.name;
            OldRecord.description = updatedItem.description;
            OldRecord.quantity = updatedItem.quantity;
            OldRecord.price = updatedItem.price;

            //need to change the state of the record from original to the modified state!!
            ORM.Entry(OldRecord).State = System.Data.Entity.EntityState.Modified;

            ORM.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult SearchItemByName(string name)
        {
            gccoffeeshopEntities ORM = new gccoffeeshopEntities();

           ViewBag.Items = ORM.items.Where(x => x.name.ToLower().Contains
           (name.ToLower())).ToList();

            return View("Index");
        }
    }
}