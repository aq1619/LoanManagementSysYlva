using LoanManagementSys.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSys
{
    internal class LoanSystem
    {
        private ProductManager productManager;
        //private MemberManager memberManager;
        //private LoanItemManager loanItemManager;


        private AdminTask adminTask;
        private UpdateGUI updateGUI;
        //private ReturnTask returnTask;
        //private LoanTask loanTask;

        private Thread adminThread;
        private Thread updateGUIThread;

        private ListBox productListBox;
        private ListBox eventLogListBox;
        public LoanSystem(ListBox productListBox, ListBox eventListBox)
        {
            productManager = new ProductManager();
            //memberManager = new MemberManager();
            //loanItemManager = new LoanItemManager();

            this.productListBox = productListBox;
            this.eventLogListBox = eventLogListBox;

            updateGUI = new UpdateGUI(this); 
            adminTask = new AdminTask(productManager);

            productManager.AddTestProducts();
        }

        public void Start()
        {
             adminThread = new Thread(new ThreadStart(adminTask.Run));
             adminThread.Start();

             updateGUIThread = new Thread(new ThreadStart(updateGUI.Run));
             updateGUIThread.Start();
        }
        public void Stop()
        {
            adminTask.IsRunning = false;
            updateGUI.IsRunning = false;
        }

        public void LoanProduct(Member member, Product product)
        {
            //LoanItem loanItem = new LoanItem(product, member);
            //loanItemManager.Add(loanItem);
        }
        public void ReturnProduct(LoanItem loanItem)
        {
            //loanItemManager.Remove(loanItem);
            //productManager.Add(loanItem.Product);
        }

        public void UpdateEventListBox()
        {

        }
        public void UpdateAllItems()
        {
            string[] productStrings = productManager.GetProductInfoStrings();
            Debug.WriteLine("Updating UI with " + productStrings.Length + " products");
            UpdateProductListBox(productStrings);
        }
        public void UpdateProductListBox(string[] productStrings)
        {
            //if (productListBox.InvokeRequired)
            //{
            //    productListBox.Invoke(new Action<string[]>(UpdateProductListBox), productStrings);
            //}
            //else
            {
                //productListBox.Items.Clear();
                //foreach (string item in productStrings)
                //{
                //    productListBox.Items.Add(item);
                //}
                //if (productListBox.Items.Count != productStrings.Length)
                //{
                //    productListBox.Items.Clear(); // Clear only if new items will be added
                //}
                //foreach (string item in productStrings)
                //{
                //    if (!productListBox.Items.Contains(item)) // Only add if not already in the list
                //    {
                //        productListBox.Items.Add(item);
                //    }
                //}

                if (productListBox.InvokeRequired)
                {
                    productListBox.Invoke(new Action<string[]>(UpdateProductListBox), productStrings);
                }
                else
                {
                    if (productListBox.Items.Count != productStrings.Length)
                    {
                        productListBox.Items.Clear(); // Clear only if new items will be added
                    }

                    foreach (string item in productStrings)
                    {
                        if (!productListBox.Items.Contains(item)) // Only add if not already in the list
                        {
                            productListBox.Items.Add(item);
                        }
                    }
                }
            }
        }
        public void WriteOutput(string message)
        {
            if (productListBox.InvokeRequired)
            {
                productListBox.Invoke(new Action<string>(WriteOutput), message);
            }
            else
            {
                productListBox.Items.Add(message);
            }
        }
    }
}
