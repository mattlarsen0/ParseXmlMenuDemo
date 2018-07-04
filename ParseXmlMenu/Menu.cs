using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ParseXmlMenu
{
    public class Menu
    {
        private const string RootMenuElement = "menu";
        private const string SubMenuItemPathElement = "subMenu";
        private const string MenuItemElement = "item";
        private const string MenuItemDisplayNameElement = "displayName";
        private const string MenuItemPathElement = "path";
        private const string MenuItemPathValueAttribute = "value";

        public Menu()
        {
            Items = new List<MenuItem>();
        }

        private List<MenuItem> Items { get; set; }

        public override string ToString()
        {
            StringBuilder menuSb = new StringBuilder();

            foreach (MenuItem item in Items)
            {
                menuSb.Append(item.ToString());
                menuSb.Append(Environment.NewLine);
                menuSb.Append(item.GetSubMenuString());
            }

            return menuSb.ToString();
        }

        public void LoadFromXml(string xmlPath, string activePath)
        {
            // load xml
            XDocument menuXml = XDocument.Load(xmlPath);

            // build menu
            BuildMenuFromXml(menuXml, activePath);
        }

        private void BuildMenuFromXml(XDocument menuXml, string activePath)
        {
            IEnumerable<XElement> xmlItems = menuXml.Element(RootMenuElement)
                                                    .Elements(MenuItemElement);

            foreach (XElement xmlItem in xmlItems)
            {
                MenuItem menuItem = GetMenuItemFromXElement(xmlItem, activePath);
                Items.Add(menuItem);
            }
        }

        private MenuItem GetMenuItemFromXElement(XElement xmlItem, string activePath)
        {
            // parse current item
            MenuItem menuItem = new MenuItem();

            menuItem.Name = xmlItem.Element(MenuItemDisplayNameElement).Value;
            menuItem.Path = xmlItem.Element(MenuItemPathElement)
                                   .Attribute(MenuItemPathValueAttribute)
                                   .Value;
            menuItem.IsActive = menuItem.Path == activePath;

            // build submenu
            IEnumerable<XElement> xmlSubMenuItems = xmlItem.Element(SubMenuItemPathElement)
                                                           ?.Elements(MenuItemElement);

            if (xmlSubMenuItems != null)
            {
                foreach (XElement xmlSubMenuItem in xmlSubMenuItems)
                {
                    MenuItem subMenuItem = GetMenuItemFromXElement(xmlSubMenuItem, activePath);
                    menuItem.SubMenuItems.Add(subMenuItem);

                    if (subMenuItem.IsActive)
                    {
                        menuItem.IsActive = true;
                    }
                }
            }

            return menuItem;
        }
    }
}