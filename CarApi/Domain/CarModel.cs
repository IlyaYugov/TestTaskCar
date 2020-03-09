using System;

namespace Domain
{
    public class CarModel
    {
        public string Id { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; }
        
        public override bool Equals(Object obj)
        {
            if ((obj == null) || ! this.GetType().Equals(obj.GetType())) 
            {
                return false;
            }
            else { 
                CarModel p = (CarModel) obj; 
                return Id == p.Id && Name == p.Name && Description == p.Description;
            }   
        }
    }
}