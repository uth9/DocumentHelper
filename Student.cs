using System;
using System.Collections;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace DocumentHelper
{

    public class Student : INotifyPropertyChanged
    {

        private string _StudentName = "";
        public string StudentName
        {
            get => this._StudentName;
            set
            {
                this._StudentName = value;
                this.onPropertyChanged(nameof(this.StudentName));
            }
        }
        private string _StudentNation = "汉族";
        public string StudentNation
        {
            get => this._StudentNation;
            set
            {
                this._StudentNation = value;
                this.onPropertyChanged(nameof(this.StudentNation));
            }
        }
        private string _Pin = "";
        public string Pin
        {
            get => _Pin;
            set
            {
                this._Pin = value;
                this.onPropertyChanged(nameof(this.Pin));
            }
        }
        private string _ReconfirmedPin = "";
        public string ReconfirmedPin
        {
            get => this._ReconfirmedPin;
            set
            {
                this._ReconfirmedPin = value;
                this.onPropertyChanged(nameof(this.ReconfirmedPin));
            }
        }
        private string _MemberId = "";
        public string MemberId
        {
            get => this._MemberId;
            set
            {
                this._MemberId = value;
                this.onPropertyChanged(nameof(this.MemberId));
            }
        }
        private string[] _RegDate = ["2025", "01"];
        public string RegDate
        {
            get => string.Concat(this._RegDate[0], "/", this._RegDate[1]);
            set { _RegDate = value.Split('/'); }
        }
        public string RegYear
        {
            get => this._RegDate[0];
            set
            {
                this._RegDate[0] = value;
                this.onPropertyChanged(nameof(this.RegYear));
                this.onPropertyChanged(nameof(this.RegDate));
            }
        }
        public string RegMonth
        {
            get => this._RegDate[1];
            set
            {
                this._RegDate[1] = value;
                this.onPropertyChanged(nameof(this.RegMonth));
                this.onPropertyChanged(nameof(this.RegDate));
            }
        }
        private string _Tel = "";
        public string Tel
        {
            get => this._Tel;
            set
            {
                this._Tel = value;
                this.onPropertyChanged(nameof(this.Tel));
            }
        }
        private string _Address = "";
        public string Address
        {
            get => this._Address;
            set
            {
                this._Address = value;
                this.onPropertyChanged(nameof(this.Address));
            }
        }
        private bool _VolunteerState = true;
        public bool VolunteerState
        {
            get => this._VolunteerState;
            set
            {
                this._VolunteerState = value;
                this.onPropertyChanged(nameof(VolunteerState));
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged is not null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

