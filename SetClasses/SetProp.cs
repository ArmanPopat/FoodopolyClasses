using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardClasses;
using FoodopolyClasses.SetClasses;
using PlayerClasses;

namespace  SetClasses;

public class SetProp:SetBase
{
    //public string Name { get; init; }
    public  List<Property> Properties { get; set; }  //turn into set only once after constructor somehow -- look into   maybe change into Ilist

    public override PlayerClass? Owner
    {
        get
        {
            if (!SetExclusivelyOwned)
            {
                return null;
            }
            else
            {
                return Properties[0].Owner;
            }
        }
    }//checks each time

    //public PlayerClass? Owner {
    //    get
    //    {
    //        if (!SetExclusivelyOwned)
    //        {
    //            return null;
    //        }
    //        else
    //        {
    //            return Properties[0].Owner;
    //        }
    //    }
    //}//checks each time

    public override bool SetExclusivelyOwned
    {
        get
        {
            if (Properties == null)
            {
                throw new ArgumentNullException(nameof(Properties));
            }
            else if (Properties.Any(o => o == null))
            {
                return false;
            }
            else
            {
                var propertyOwners = Properties.Select(o => o.Owner).ToList();
                if (!propertyOwners.Any(o => o != propertyOwners[0]))
                {
                    //Console.WriteLine("Hellllo");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    } /// checks each time
    public SetProp(string name, List<Property> properties):base(name)
    {
        //Name = name;
        Properties = properties;
        //SetExclusivelyOwned = false;
        //Owner = null;
    }

    //method to verify OwnsSet and Owner props are correct
    //public void VerifySetOwnedProps()
    //{
    //    if (Properties == null)
    //    {
    //        throw new ArgumentNullException(nameof(Properties));
    //    }
    //    else if (Properties.Any(o => o == null))
    //    {
    //        SetExclusivelyOwned = false;
    //        Owner = null;
    //    }
    //    else
    //    {
    //        var propertyOwners = Properties.Select(o => o.Owner).ToList();
    //        if (!propertyOwners.Any(o => o != propertyOwners[0]))
    //        {
    //            //Console.WriteLine("Hellllo");
    //            SetExclusivelyOwned = true;
    //            Owner = propertyOwners[0];
    //        }
    //        else
    //        {
    //            SetExclusivelyOwned = false;
    //            Owner = null;
    //        }
    //    }
    //    //foreach (var player in propertyOwners)
    //    //{
    //    //    if (player != null)
    //    //    {
    //    //        Console.WriteLine(player.Name);
    //    //    }
    //    //}
    //    //foreach (Property property in Properties)
    //    //{
    //    //    property.Owner = ;
    //    //}
    //}
}
