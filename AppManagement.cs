using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
    interface AppManagement
    {
        public void addItem(string connectionString, string task, bool status);
        public void showItems(string connectionString);
    }
}
