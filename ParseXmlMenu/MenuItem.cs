using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseXmlMenu
{
    public class MenuItem
    {
        /// <summary>
        /// Four spaces used to indent sub menus
        /// </summary>
        private const string Indent = "    ";

        private const string ActiveItem = "ACTIVE";

        public MenuItem()
        {
            SubMenuItems = new List<MenuItem>();
        }

        public string Name { get; set; }

        public string Path { get; set; }

        public bool IsActive { get; set; }

        public List<MenuItem> SubMenuItems { get; set; }

        public override string ToString()
        {
            return Name + ", " + Path + (IsActive ? " " + ActiveItem : "");
        }

        public string GetSubMenuString(int depth = 1)
        {
            StringBuilder subMenuSb = new StringBuilder();

            foreach (MenuItem subMenuItem in SubMenuItems)
            {
                // insert tabs
                for (int i = 0; i < depth; i++)
                {
                    subMenuSb.Append(Indent);
                }

                subMenuSb.Append(subMenuItem.ToString());
                subMenuSb.Append(Environment.NewLine);

                subMenuSb.Append(subMenuItem.GetSubMenuString(depth + 1));
            }

            return subMenuSb.ToString();
        }
    }
}