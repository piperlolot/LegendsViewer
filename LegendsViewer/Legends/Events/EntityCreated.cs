using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;
using System.Linq;

namespace LegendsViewer.Legends.Events
{
    public class EntityCreated : WorldEvent
    {
        public Entity Entity { get; set; }
        public Site Site { get; set; }
        public int StructureID { get; set; }
        public Structure Structure { get; set; }

        public EntityCreated(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "entity_id": Entity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "structure_id": StructureID = Convert.ToInt32(property.Value); break;
                }
            }
            if (Site != null)
            {
                Structure = Site.Structures.FirstOrDefault(structure => structure.ID == StructureID);
            }
            Entity.AddEvent(this);
            Site.AddEvent(this);
            Structure.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            eventString += Entity.ToLink(link, pov) + " formed";
            if (Structure != null)
            {
                eventString += " in ";
                eventString += Structure.ToLink(link, pov);
            }
            if (Site != null)
            {
                eventString += " in ";
                eventString += Site.ToLink(link, pov);
            }
            eventString += PrintParentCollection(link, pov);
            eventString += ".";
            return eventString;
        }
    }
}