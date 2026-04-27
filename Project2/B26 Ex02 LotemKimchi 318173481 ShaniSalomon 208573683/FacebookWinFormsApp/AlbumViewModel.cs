using System.ComponentModel;

namespace BasicFacebookFeatures
{
    // Two-Way Data Binding support:
    // Implements INotifyPropertyChanged so that when PhotoCount is updated
    // (e.g. after an upload), the DataGridView reflects the new value automatically
    // without a full grid reload.
    public class AlbumViewModel : INotifyPropertyChanged
    {
        private string m_Name;
        private int m_PhotoCount;

        public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
                onPropertyChanged("Name");
            }
        }

        public int PhotoCount
        {
            get { return m_PhotoCount; }
            set
            {
                m_PhotoCount = value;
                onPropertyChanged("PhotoCount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(string i_PropertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(i_PropertyName));
            }
        }
    }
}
