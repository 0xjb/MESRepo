using MES.Acquintance;

namespace MES.Data
{
    public class BatchValue : IBatchValue
    {
        private string timestamp;
        private float value;
        private int type;

        public BatchValue(float _value, string _timestamp, int _type)
        {
            value = _value;
            timestamp = _timestamp;
            type = _type;
        }

        public string Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public float Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
