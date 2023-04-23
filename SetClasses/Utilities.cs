﻿using BoardClasses;
using PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodopolyClasses.SetClasses;

public class Utilities:SetBase
{
    public List<Utility> Properties { get; set; }

    public Utilities(List<Utility> properties) : base("Utilities")
    {
        Properties = properties;
    }
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
}
