using System;
using System.Configuration;

namespace Log
{
    public class Config : ConfigurationSection
    {
        [ConfigurationProperty("direction")]
        public Direction Direction
        {
            get
            {
                Direction direction = (Direction)this["direction"];
                return direction ?? new Direction();
            }
        }

        [ConfigurationProperty("fileNameFormat")]
        public FileNameFormat FileNameFormat
        {
            get
            {
                FileNameFormat fileNameFormat = (FileNameFormat)this["fileNameFormat"];
                return fileNameFormat ?? new FileNameFormat();
            }
        }

        [ConfigurationProperty("extension")]
        public Extension Extension
        {
            get
            {
                Extension extension = (Extension)this["extension"];
                return extension ?? new Extension();
            }
        }

        [ConfigurationProperty("maximumFileSize")]
        public MaximumFileSize MaximumFileSize
        {
            get
            {
                MaximumFileSize maximumFileSize = (MaximumFileSize)this["maximumFileSize"];
                return maximumFileSize ?? new MaximumFileSize();
            }
        }

        [ConfigurationProperty("dateTimeFormat")]
        public DateTimeFormat DateTimeFormat
        {
            get
            {
                DateTimeFormat dateTimeFormat = (DateTimeFormat)this["dateTimeFormat"];
                return dateTimeFormat ?? new DateTimeFormat();
            }
        }
    }

    public class Direction : ConfigurationElement
    {
        private string _value = "logs";

        [ConfigurationProperty("value", DefaultValue = "logs", IsRequired = true)]
        public string Value
        {
            get
            {
                string value = (string)this["value"];
                return value ?? _value;
            }
        }
    }

    public class FileNameFormat : ConfigurationElement
    {
        private string _value = "yyyy.MM.dd";

        [ConfigurationProperty("value", DefaultValue = "yyyy.MM.dd", IsRequired = true)]
        public string Value
        {
            get
            {
                string value = (string)this["value"];
                return value ?? _value;
            }
        }
    }

    public class Extension : ConfigurationElement
    {
        private string _value = "log";

        [ConfigurationProperty("value", DefaultValue = "log", IsRequired = true)]
        public string Value
        {
            get
            {
                string value = (string)this["value"];
                return value ?? _value;
            }
        }
    }

    public class MaximumFileSize : ConfigurationElement
    {
        private double _value = 20;

        [ConfigurationProperty("value", DefaultValue = "20", IsRequired = true)]
        public double Value
        {
            get
            {
                double? value = (double?)this["value"];
                return value ?? _value;
            }
        }
    }

    public class DateTimeFormat : ConfigurationElement
    {
        private string _value = "yyyy-MM-dd HH:mm:ss.fff";

        [ConfigurationProperty("value", DefaultValue = "yyyy-MM-dd HH:mm:ss.fff", IsRequired = true)]
        public String Value
        {
            get
            {
                string value = (string)this["value"];
                return value ?? _value;
            }
        }
    }
}