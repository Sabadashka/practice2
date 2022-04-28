namespace Staff_Project
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            Main logic = new Main();

            while (true)
            {
                var action = logic.PrintMenu();
                if (action.Item2 == 0) break; // if exit

                if(action.Item1 == "BaseMenu")
                {
                    if (action.Item2 == 1) logic.Login();
                    else if (action.Item2 == 2) logic.Registration();
                }
                else if(action.Item1 == "StaffMenu")
                {
                    if (action.Item2 == 1) logic.CreateNewPayment();
                    else if (action.Item2 == 2) logic.EditPaymentWithDraftStatus();
                    else if (action.Item2 == 3) logic.DeletePaymentWithDraftStatus();
                    else if (action.Item2 == 4) logic.PrintAllUserPayments();
                    else if (action.Item2 == 5) logic.PrintAllUserPaymentsByFilter();
                    else if (action.Item2 == 6) logic.PrintAllApprovedPayments();
                    else if (action.Item2 == 7) logic.EditAndSendForModeration();
                    else if (action.Item2 == 8) logic.LogOut();
                }
                else if(action.Item1 == "AdminMenu")
                {
                    if (action.Item2 == 1) logic.CreateNewPayment();
                    else if (action.Item2 == 2) logic.EditPaymentWithDraftStatus();
                    else if (action.Item2 == 3) logic.DeletePaymentWithDraftStatus();
                    else if (action.Item2 == 4) logic.PrintAllUserPayments();
                    else if (action.Item2 == 5) logic.PrintAllUserPaymentsByFilter();
                    else if (action.Item2 == 6) logic.PrintAllApprovedPayments();
                    else if (action.Item2 == 7) logic.EditAndSendForModeration();
                    else if (action.Item2 == 8) logic.LogOut();
                    else if (action.Item2 == 9) logic.AdminPanelPrintAllPayments();
                    else if (action.Item2 == 10) logic.AdminPanelPrintAllRejectedPayments();
                    else if (action.Item2 == 11) logic.AdminPanelSetStatusOfPayment();
                }
            }
        }
    }
}
