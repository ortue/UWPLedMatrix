//using ManagedBass;
//using ManagedBass.Wasapi;
//using System;
//using System.Collections.Generic;

//namespace WebMatrix.Classes
//{
//  public class AudioSpectrum
//  {

//    private bool _enable;               //enabled status
//    //private DispatcherTimer _t;         //timer that refreshes the display
//    public float[] _fft;               //buffer for fft data
//    //private ProgressBar _l, _r;         //progressbars for left and right channel intensity
//    private WasapiProcedure _process;        //callback function to obtain data
//    private int _lastlevel;             //last output level
//    private int _hanctr;                //last output level counter
//    public List<byte> _spectrumdata;   //spectrum data buffer
//    //private Spectrum _spectrum;         //spectrum dispay control
//    private List<string> _devicelist { get; set; }       //device list
//    private bool _initialized;          //initialized flag
//    private int devindex;               //used device index

//    private int SelectedDevice { get; set; }
//    private int _lines = 20;            // number of spectrum lines



//    public AudioSpectrum() //ProgressBar left, ProgressBar right, Spectrum spectrum, ComboBox devicelist, Chart chart
//    {
//      _fft = new float[8192];
//      _lastlevel = 0;
//      _hanctr = 0;
//      //_t = new DispatcherTimer();
//      //_t.Tick += _t_Tick;
//      //_t.Interval = TimeSpan.FromMilliseconds(25); //40hz refresh rate//25
//      //_t.IsEnabled = false;
//      //_l = left;
//      //_r = right;
//      //_l.Minimum = 0;
//      //_r.Minimum = 0;
//      //_r.Maximum = (ushort.MaxValue);
//      //_l.Maximum = (ushort.MaxValue);
//      //_process = new WASAPIPROC(Process);

//      _process = new WasapiProcedure(Process);

//      _spectrumdata = new List<byte>();
//      //_spectrum = spectrum;
//      _devicelist = new List<string>();
//      _initialized = false;

//      Init();
//    }

//    // flag for display enable
//    public bool DisplayEnable { get; set; }

//    //flag for enabling and disabling program functionality
//    public bool Enable
//    {
//      get { return _enable; }
//      set
//      {
//        _enable = value;

//        if (value)
//        {
//          if (!_initialized)
//          {
//            string[] array = (_devicelist[SelectedDevice]).Split(' ');
//            devindex = Convert.ToInt32(array[0]);
//            //bool result = BassWasapi.BASS_WASAPI_Init(devindex, 0, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER, 1f, 0.05f, _process, IntPtr.Zero);

//            bool result = BassWasapi.Init(devindex, 0, 0, WasapiInitFlags.Buffer, 1f, 0.05f, _process, IntPtr.Zero);

//            if (!result)
//            {
//              Errors error = Bass.LastError;
//              //MessageBox.Show(error.ToString());
//            }
//            else
//            {
//              _initialized = true;
//              //_devicelist.Enabled = false;
//            }
//          }

//          BassWasapi.Start();
//        }
//        else
//          BassWasapi.Stop(true);

//        //  System.Threading.Thread.Sleep(500);
//        // _t.IsEnabled = value;
//      }
//    }

//    // initialization
//    private void Init()
//    {
//      for (int i = 0; i < BassWasapi.DeviceCount; i++)
//      {
//        WasapiDeviceInfo device = BassWasapi.GetDeviceInfo(i);

//        if (device.IsEnabled && device.IsLoopback)
//          _devicelist.Add(string.Format("{0} - {1}", i, device.Name));
//      }

//      SelectedDevice = 0;
//      //Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATETHREADS, false);

//      Bass.Configure(Configuration.UpdateThreads, false);

//      //bool result = Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);

//      bool result = Bass.Init(0, 44100, DeviceInitFlags.Default, IntPtr.Zero);

//      if (!result)
//        throw new Exception("Init Error");
//    }

//    //timer 
//    public List<int> _t_Tick()
//    {
//      //int ret = BassWasapi.BASS_WASAPI_GetData(_fft, (int)BASSData.BASS_DATA_FFT8192);  //get ch.annel fft data

//      int ret = BassWasapi.GetData(_fft, -2147483643);

//      if (ret < -1)
//        return null;

//      int x, y;
//      int b0 = 0;

//      //computes the spectrum data, the code is taken from a bass_wasapi sample.
//      for (x = 0; x < _lines; x++)
//      {
//        float peak = 0;
//        int b1 = (int)Math.Pow(2, x * 10.0 / (_lines - 1));

//        if (b1 > 1023)
//          b1 = 1023;

//        if (b1 <= b0)
//          b1 = b0 + 1;

//        for (; b0 < b1; b0++)
//        {
//          if (peak < _fft[1 + b0])
//            peak = _fft[1 + b0];
//        }

//        y = (int)(Math.Sqrt(peak) * 3 * 255 - 4);

//        if (y > 255)
//          y = 255;

//        if (y < 0)
//          y = 0;

//        _spectrumdata.Add((byte)y);
//        //Console.Write("{0, 3} ", y);
//      }

//      //if (DisplayEnable)
//      //  _spectrum.Set(_spectrumdata);



//      //C'est ici qu'on transfert le array de _spectrumdata dans les led

//      List<int> spectrum = new List<int>();

//      foreach (byte data in _spectrumdata)
//        spectrum.Add((int)(19 - (data / 13.42)));

//      _spectrumdata.Clear();

//      int level = BassWasapi.GetLevel();

//      if (level == _lastlevel && level != 0)
//        _hanctr++;

//      _lastlevel = level;

//      //Required, because some programs hang the output. If the output hangs for a 75ms
//      //this piece of code re initializes the output so it doesn't make a gliched sound for long.
//      if (_hanctr > 3)
//      {
//        _hanctr = 0;
//        Free();
//        //Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
//        Bass.Init(0, 44100, DeviceInitFlags.Default, IntPtr.Zero);


//        _initialized = false;
//        Enable = true;
//      }

//      return spectrum;
//    }

//    // WASAPI callback, required for continuous recording
//    private int Process(IntPtr buffer, int length, IntPtr user)
//    {
//      return length;
//    }

//    //cleanup
//    public void Free()
//    {
//      BassWasapi.Free();
//      Bass.Free();
//    }
//  }
//}