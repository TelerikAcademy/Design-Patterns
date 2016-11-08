using System;

using SchoolSystem.Framework.Models.Contracts;
using SchoolSystem.Framework.Models.Enums;

namespace SchoolSystem.Framework.Models
{
    public class Mark : IMark
    {
        private const float MinValue = 2.00f;
        private const float MaxValue = 6.00f;

        private float value;

        public Mark(Subject subject, float value)
        {
            this.Subject = subject;
            this.Value = value;
        }

        public Subject Subject { get; private set; }

        public float Value
        {
            get
            {
                return this.value;
            }

            private set
            {
                if (value < MinValue || value > MaxValue)
                {
                    throw new ArgumentOutOfRangeException($"The value of the mark must be between {MinValue} and {MaxValue}");
                }

                this.value = value;
            }
        }
    }
}
