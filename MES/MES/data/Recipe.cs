using MES.Acquintance;

namespace MES.Data
{
    public class Recipe : IRecipe
    {
        private float beerId;
        private float maxSpeed;
        private string name;
        private float barley;
        private float hops;
        private float malt;
        private float wheat;
        private float yeast;

        public float BeerId
        {
            get => beerId;
            set => beerId = value;
        }

        public float MaxSpeed
        {
            get => maxSpeed;
            set => maxSpeed = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public float Barley
        {
            get => barley;
            set => barley = value;
        }

        public float Hops
        {
            get => hops;
            set => hops = value;
        }

        public float Malt
        {
            get => malt;
            set => malt = value;
        }

        public float Wheat
        {
            get => wheat;
            set => wheat = value;
        }

        public float Yeast
        {
            get => yeast;
            set => yeast = value;
        }

        public Recipe(float beerId, float maxSpeed, string name,
            float barley, float hops, float malt, float wheat, float yeast)
        {
            this.beerId = beerId;
            this.maxSpeed = maxSpeed;
            this.name = name;
            this.barley = barley;
            this.hops = hops;
            this.malt = malt;
            this.wheat = wheat;
            this.yeast = yeast;
        }

        override
        public string ToString()
        {
            return beerId + ": " + name;
        }
    }
}
