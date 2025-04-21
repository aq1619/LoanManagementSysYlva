using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSys;

/*
  The task of this thread class is to send request to the controller to
  update the list of products that are on loand as wekk as  the list
  available products on the UI using at certain intervals (e.g 2 seconds).

  Note: loanSys is a reference to an object of the LoanSystem which
  is a class which creates the threads and also updates the MainForm.
  It has the reference to the listboxes for updating.
*/
internal class UpdateGUI
{
    private Random random;
    private bool isRunning = true; //to start and stop the thread
    private LoanSystem loanSys;

    //constructor
    public UpdateGUI(LoanSystem loanSys)
    {
        this.loanSys = loanSys;
        random = new Random();
    }

    //Sets isRunning to true/fals; when true, the thread continues processing and
    //if false, it stops
    public bool IsRunning { get; set; } = true;

    public void Run()
    {
        try
        {
            while (IsRunning)
            {
                Debug.WriteLine("updating GUI " + DateTime.Now);
                
                Thread.Sleep(1000); 
                loanSys.UpdateAllItems();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in UpdateGUI.Run: " + ex.Message);
        }


    }

    public void Stop()
    {
        IsRunning = false;
    }
}

