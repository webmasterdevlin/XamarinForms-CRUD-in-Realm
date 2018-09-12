using Realms;

namespace RealmXF.Models
{
    public class Recipe : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
