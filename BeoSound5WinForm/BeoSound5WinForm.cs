using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;
using System.Windows.Interop;

namespace BeoSound5WinForm
{
    public partial class BeoSound5WinForm : System.Windows.Forms.Form
    {
        //==========================================================================================================================================================================================================
        // DATA      DATA      DATA      DATA      DATA      DATA      DATA      DATA      DATA      DATA      DATA      DATA      DATA      DATA      DATA      DATA      DATA      DATA      DATA      DATA
        //==========================================================================================================================================================================================================
        #region WinAPI

            [DllImport("setupapi.dll")]
            static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, IntPtr Enumerator, IntPtr hwndParent, int Flags);

		    [DllImport("setupapi.dll")]
            static extern bool SetupDiEnumDeviceInterfaces(IntPtr hDevInfo, IntPtr devInfo, ref Guid interfaceClassGuid, int memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

            [DllImport(@"setupapi.dll")]
            static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, ref SP_DEVICE_INTERFACE_DETAIL_DATA DeviceInterfaceDetailData, int DeviceInterfaceDetailDataSize, ref int RequiredSize, IntPtr DeviceInfoData);

            [DllImport(@"setupapi.dll")]
            static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, IntPtr DeviceInterfaceDetailData, int DeviceInterfaceDetailDataSize, ref int RequiredSize, IntPtr DeviceInfoData);

            [DllImport(@"kernel32.dll")]
            static extern IntPtr CreateFile(string fileName, uint fileAccess, uint fileShare, FileMapProtection securityAttributes, uint creationDisposition, uint flags, IntPtr overlapped);

            [DllImport("kernel32.dll")]
            static extern bool WriteFile(IntPtr hFile, [Out] byte[] lpBuffer, uint nNumberOfBytesToWrite, ref uint lpNumberOfBytesWritten, IntPtr lpOverlapped);

		    [DllImport("kernel32.dll")]
            static extern bool ReadFile(IntPtr hFile, [Out] byte[] lpBuffer, uint nNumberOfBytesToRead, ref uint lpNumberOfBytesRead, IntPtr lpOverlapped);

            [DllImport("hid.dll")]
            static extern void HidD_GetHidGuid(ref Guid Guid);

            [DllImport("hid.dll")]
            static extern bool HidD_GetPreparsedData(IntPtr HidDeviceObject, ref IntPtr PreparsedData);

            [DllImport("hid.dll")]
            static extern bool HidD_GetAttributes(IntPtr DeviceObject, ref HIDD_ATTRIBUTES Attributes);

            [DllImport("hid.dll")]
            static extern uint HidP_GetCaps(IntPtr PreparsedData, ref HIDP_CAPS Capabilities);

            [DllImport("hid.dll")]
            static extern int HidP_GetButtonCaps(HIDP_REPORT_TYPE ReportType, [In, Out] HIDP_BUTTON_CAPS[] ButtonCaps, ref ushort ButtonCapsLength, IntPtr PreparsedData);
        
            [DllImport("hid.dll")]
            static extern int HidP_GetValueCaps(HIDP_REPORT_TYPE ReportType, [In, Out] HIDP_VALUE_CAPS[] ValueCaps, ref ushort ValueCapsLength, IntPtr PreparsedData);

            [DllImport("hid.dll")]
            static extern int HidP_MaxUsageListLength(HIDP_REPORT_TYPE ReportType, ushort UsagePage, IntPtr PreparsedData);

            [DllImport("hid.dll")]
            static extern int HidP_SetUsages(HIDP_REPORT_TYPE ReportType, ushort UsagePage, short LinkCollection, short Usages, ref int UsageLength, IntPtr PreparsedData, IntPtr HID_Report, int ReportLength);
        
            [DllImport("hid.dll")]
            static extern int HidP_SetUsageValue(HIDP_REPORT_TYPE ReportType, ushort UsagePage, short LinkCollection, ushort Usage, ulong UsageValue, IntPtr PreparsedData, IntPtr HID_Report, int ReportLength);

            [DllImport("setupapi.dll")]
            static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

            [DllImport("kernel32.dll")]
            static extern bool CloseHandle(IntPtr hObject);

            [DllImport("hid.dll")]
            static extern bool HidD_FreePreparsedData(ref IntPtr PreparsedData);

            [DllImport("hid.dll")]
            private static extern bool HidD_GetProductString(IntPtr HidDeviceObject, IntPtr Buffer, uint BufferLength);

            [DllImport("hid.dll")]
            static extern bool HidD_GetSerialNumberString(IntPtr HidDeviceObject, IntPtr Buffer, Int32 BufferLength);

            [DllImport("hid.dll")]
            static extern Boolean HidD_GetManufacturerString(IntPtr HidDeviceObject, IntPtr Buffer, Int32 BufferLength);

            [DllImport("kernel32.dll")]
            static extern uint GetLastError();

        #endregion

        #region DLL Var

            IntPtr  hardwareDeviceInfo;

            const int  DIGCF_DEFAULT            = 0x00000001;
            const int  DIGCF_PRESENT            = 0x00000002;
            const int  DIGCF_ALLCLASSES         = 0x00000004;
            const int  DIGCF_PROFILE            = 0x00000008;
            const int  DIGCF_DEVICEINTERFACE    = 0x00000010;
                                             
            const uint GENERIC_READ             = 0x80000000;
            const uint GENERIC_WRITE            = 0x40000000;
            const uint GENERIC_EXECUTE          = 0x20000000;
            const uint GENERIC_ALL              = 0x10000000;
                                             
            const uint FILE_SHARE_READ          = 0x00000001;  
            const uint FILE_SHARE_WRITE         = 0x00000002;  
            const uint FILE_SHARE_DELETE        = 0x00000004;  

            const uint CREATE_NEW               = 1;
            const uint CREATE_ALWAYS            = 2;
            const uint OPEN_EXISTING            = 3;
            const uint OPEN_ALWAYS              = 4;
            const uint TRUNCATE_EXISTING        = 5;

            const int  HIDP_STATUS_SUCCESS      = 1114112;
            const int  DEVICE_PATH              = 260;
            const int  INVALID_HANDLE_VALUE     = -1;

            enum FileMapProtection : uint
            {
                PageReadonly         = 0x02,
                PageReadWrite        = 0x04,
                PageWriteCopy        = 0x08,
                PageExecuteRead      = 0x20,
                PageExecuteReadWrite = 0x40,
                SectionCommit        = 0x8000000,
                SectionImage         = 0x1000000,
                SectionNoCache       = 0x10000000,
                SectionReserve       = 0x4000000,
            }

            enum HIDP_REPORT_TYPE : ushort
            {
                HidP_Input   = 0x00,
                HidP_Output  = 0x01,
                HidP_Feature = 0x02,
            }

            [StructLayout(LayoutKind.Sequential)]
            struct LIST_ENTRY
            {
                public IntPtr Flink;
                public IntPtr Blink;
            }

            [StructLayout(LayoutKind.Sequential)]
            struct DEVICE_LIST_NODE
            {
                public LIST_ENTRY      Hdr;
                public IntPtr          NotificationHandle;
                public HID_DEVICE      HidDeviceInfo;
                public bool            DeviceOpened;
            }

            [StructLayout(LayoutKind.Sequential)]
            struct SP_DEVICE_INTERFACE_DATA
            {
                public  Int32   cbSize;
                public  Guid    interfaceClassGuid;
                public  Int32   flags;
                private UIntPtr reserved;
            }
        
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            struct SP_DEVICE_INTERFACE_DETAIL_DATA
            {
                public int cbSize;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = DEVICE_PATH)]
                public string DevicePath;
            }    

            [StructLayout(LayoutKind.Sequential)]
            struct SP_DEVINFO_DATA
            {
               public int       cbSize;
               public Guid      classGuid;
               public UInt32    devInst;
               public IntPtr    reserved;
            }

            [StructLayout(LayoutKind.Sequential)]
            struct HIDP_CAPS
            {
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 Usage;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 UsagePage;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 InputReportByteLength;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 OutputReportByteLength;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 FeatureReportByteLength;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
                public UInt16[] Reserved;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 NumberLinkCollectionNodes;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 NumberInputButtonCaps;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 NumberInputValueCaps;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 NumberInputDataIndices;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 NumberOutputButtonCaps;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 NumberOutputValueCaps;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 NumberOutputDataIndices;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 NumberFeatureButtonCaps;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 NumberFeatureValueCaps;
                [MarshalAs(UnmanagedType.U2)]
                public UInt16 NumberFeatureDataIndices;
            };

            [StructLayout(LayoutKind.Sequential)]
            struct HIDD_ATTRIBUTES
            { 
                public Int32    Size; 
                public UInt16   VendorID; 
                public UInt16   ProductID; 
                public Int16    VersionNumber; 
            }

            [StructLayout(LayoutKind.Sequential)]
            struct ButtonData
            {
                 public Int32 UsageMin;
                 public Int32 UsageMax;
                 public Int32 MaxUsageLength; 
                 public Int16 Usages;
            }

            [StructLayout(LayoutKind.Sequential)]
            struct ValueData
            {
                 public ushort  Usage;
                 public ushort  Reserved;

                 public ulong   Value;
                 public long    ScaledValue;
            }

            [StructLayout(LayoutKind.Explicit)]
            struct HID_DATA
            {
                [FieldOffset(0)]
                public bool     IsButtonData;
                [FieldOffset(1)]
                public byte     Reserved;
                [FieldOffset(2)]
                public ushort UsagePage;
                [FieldOffset(4)]
                public Int32    Status;
                [FieldOffset(8)]
                public Int32    ReportID;
                [FieldOffset(16)]
                public bool     IsDataSet;

                [FieldOffset(17)]
                public ButtonData ButtonData;
                [FieldOffset(17)]
                public ValueData ValueData;
            }

            [StructLayout(LayoutKind.Sequential)]
            struct HIDP_Range
            {
                public ushort UsageMin,         UsageMax;
                public ushort StringMin,        StringMax;
                public ushort DesignatorMin,    DesignatorMax;
                public ushort DataIndexMin,     DataIndexMax;
            }

            [StructLayout(LayoutKind.Sequential)]
            struct HIDP_NotRange
            {
                public ushort Usage,            Reserved1;
                public ushort StringIndex,      Reserved2;
                public ushort DesignatorIndex,  Reserved3;
                public ushort DataIndex,        Reserved4;
            }

            [StructLayout(LayoutKind.Explicit)]
            struct HIDP_BUTTON_CAPS
            {
                [FieldOffset(0)]
                public ushort UsagePage;
                [FieldOffset(2)]
                public byte ReportID;
                [FieldOffset(3), MarshalAs(UnmanagedType.U1)]
                public bool IsAlias;
                [FieldOffset(4)]
                public short BitField;
                [FieldOffset(6)]
                public short LinkCollection;
                [FieldOffset(8)]
                public short LinkUsage;
                [FieldOffset(10)]
                public short LinkUsagePage;
                [FieldOffset(12), MarshalAs(UnmanagedType.U1)]
                public bool IsRange;
                [FieldOffset(13), MarshalAs(UnmanagedType.U1)]
                public bool IsStringRange;
                [FieldOffset(14), MarshalAs(UnmanagedType.U1)]
                public bool IsDesignatorRange;
                [FieldOffset(15), MarshalAs(UnmanagedType.U1)]
                public bool IsAbsolute;
                [FieldOffset(16), MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
                public int[] Reserved;
                [FieldOffset(56)]
                public HIDP_Range Range;
                [FieldOffset(56)]
                public HIDP_NotRange NotRange;
            }

            [StructLayout(LayoutKind.Explicit)]
            struct HIDP_VALUE_CAPS
            {
                [FieldOffset(0)]
                public ushort UsagePage;
                [FieldOffset(2)]
                public byte ReportID;
                [FieldOffset(3), MarshalAs(UnmanagedType.U1)]
                public bool IsAlias;
                [FieldOffset(4)]
                public ushort BitField;
                [FieldOffset(6)]
                public ushort LinkCollection;
                [FieldOffset(8)]
                public ushort LinkUsage;
                [FieldOffset(10)]
                public ushort LinkUsagePage;
                [FieldOffset(12), MarshalAs(UnmanagedType.U1)]
                public bool IsRange;
                [FieldOffset(13), MarshalAs(UnmanagedType.U1)]
                public bool IsStringRange;
                [FieldOffset(14), MarshalAs(UnmanagedType.U1)]
                public bool IsDesignatorRange;
                [FieldOffset(15), MarshalAs(UnmanagedType.U1)]
                public bool IsAbsolute;
                [FieldOffset(16), MarshalAs(UnmanagedType.U1)]
                public bool HasNull;
                [FieldOffset(17)]
                public byte Reserved;
                [FieldOffset(18)]
                public short BitSize;
                [FieldOffset(20)]
                public short ReportCount;
                [FieldOffset(22)]
                public ushort Reserved2a;
                [FieldOffset(24)]
                public ushort Reserved2b;
                [FieldOffset(26)]
                public ushort Reserved2c;
                [FieldOffset(28)]
                public ushort Reserved2d;
                [FieldOffset(30)]
                public ushort Reserved2e;
                [FieldOffset(32)]
                public int UnitsExp;
                [FieldOffset(36)]
                public int Units;
                [FieldOffset(40)]
                public int LogicalMin;
                [FieldOffset(44)]
                public int LogicalMax;
                [FieldOffset(48)]
                public int PhysicalMin;
                [FieldOffset(52)]
                public int PhysicalMax;
                [FieldOffset(56)]
                public HIDP_Range Range;
                [FieldOffset(56)]
                public HIDP_NotRange NotRange;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            struct HID_DEVICE
            {
                public String             Manufacturer;
                public String             Product;
                public Int32              SerialNumber;
                public UInt16             VersionNumber;

                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = DEVICE_PATH)]
                public String             DevicePath;
                public IntPtr             Pointer;

                public Boolean            OpenedForRead;
                public Boolean            OpenedForWrite;
                public Boolean            OpenedOverlapped;
                public Boolean            OpenedExclusive;

                public IntPtr             Ppd;
                public HIDP_CAPS          Caps;
                public HIDD_ATTRIBUTES    Attributes;

                public IntPtr[]           InputReportBuffer;
                public HID_DATA[]         InputData;
                public Int32              InputDataLength;
                public HIDP_BUTTON_CAPS[] InputButtonCaps;
                public HIDP_VALUE_CAPS[]  InputValueCaps;

                public IntPtr[]           OutputReportBuffer;
                public HID_DATA[]         OutputData;
                public Int32              OutputDataLength;
                public HIDP_BUTTON_CAPS[] OutputButtonCaps;
                public HIDP_VALUE_CAPS[]  OutputValueCaps;

                public IntPtr[]           FeatureReportBuffer;
	            public HID_DATA[]         FeatureData;
                public Int32              FeatureDataLength;
                public HIDP_BUTTON_CAPS[] FeatureButtonCaps;
                public HIDP_VALUE_CAPS[]  FeatureValueCaps;
            }

        #endregion

        struct HIDReadData
        {
            public static Boolean   State;

            public static HID_DEVICE[] Device;
            public static Int32        iDevice;

            public static UInt16    VendorID;
            public static UInt16    ProductID;
        }
        struct HIDWriteData
        {
            public static Boolean   State;

            public static HID_DEVICE[] Device;
            public static Int32        iDevice;

            public static UInt16    VendorID;
            public static UInt16    ProductID;
            public static UInt16    UsagePage;
            public static UInt16    Usage;
            public static Byte      ReportID;
        }

        Byte[]  WriteData = new Byte[7];
        //==========================================================================================================================================================================================================
        // END_DATA      END_DATA      END_DATA      END_DATA      END_DATA      END_DATA      END_DATA      END_DATA      END_DATA      END_DATA      END_DATA      END_DATA      END_DATA      END_DATA
        //==========================================================================================================================================================================================================
        
        //==========================================================================================================================================================================================================
        // CODE      CODE      CODE      CODE      CODE      CODE      CODE      CODE      CODE      CODE      CODE      CODE      CODE      CODE      CODE      CODE      CODE      CODE      CODE      CODE
        //==========================================================================================================================================================================================================
        public  BeoSound5WinForm()
        {
            InitializeComponent();
            Load += new EventHandler(Entry);
        }
        void    Entry(object sender, EventArgs Data)
        {
            UInt16.TryParse(SendHID_VID_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDWriteData.VendorID);
            UInt16.TryParse(SendHID_PID_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDWriteData.ProductID);
            UInt16.TryParse(SendHID_UsagePage_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDWriteData.UsagePage);
            UInt16.TryParse(SendHID_Usage_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDWriteData.Usage);
            Byte.TryParse(SendHID_RID_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDWriteData.ReportID);
            CheckHIDWrite();
            WriteData[0] = 65;
            HIDWrite(HIDWriteData.Device[HIDWriteData.iDevice]);
            var ReadThread          = new Thread(new ThreadStart(Update));
            ReadThread.IsBackground = true;
            ReadThread.Start();
            HIDReadData.State = false;
        }
        void    Update()
        {
            while (true)
            {
                CheckHIDRead();
                //CheckHIDWrite();

                //Thread.Sleep(1);
            }
        }
        //==========================================================================================================================================================================================================
        // END_CODE      END_CODE      END_CODE      END_CODE      END_CODE      END_CODE      END_CODE      END_CODE      END_CODE      END_CODE      END_CODE      END_CODE      END_CODE      END_CODE
        //==========================================================================================================================================================================================================

        //==========================================================================================================================================================================================================
        // FUNCTION      FUNCTION      FUNCTION      FUNCTION      FUNCTION      FUNCTION      FUNCTION      FUNCTION      FUNCTION      FUNCTION      FUNCTION      FUNCTION      FUNCTION      FUNCTION
        //==========================================================================================================================================================================================================
        void    CheckHIDRead()
        {
            if (HIDReadData.State == false)
            {
                var nbrDevice  = FindDeviceNumber();
                HIDReadData.Device = new HID_DEVICE[nbrDevice];
                FindKnownHIDDevices(ref HIDReadData.Device);

                UInt16.TryParse(ReadHID_VID_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDReadData.VendorID);
                UInt16.TryParse(ReadHID_PID_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDReadData.ProductID);

                for (var Index = 0; Index < nbrDevice; Index++)
                {
                    if (HIDReadData.VendorID != 0)
                    {
                        var Count = 0;

                        if (HIDReadData.Device[Index].Attributes.VendorID == HIDReadData.VendorID)
                        {
                            Count++;
                        }
                        if (HIDReadData.Device[Index].Attributes.ProductID == HIDReadData.ProductID)
                        {
                            Count++;
                        }

                        if (Count == 2)
                        {
                            HIDReadData.iDevice = Index;
                            HIDReadData.State   = true;

                            break;
                        }
                        else
                        {
                            HIDReadData.State = false;
                        }
                    }
                }
            }
            else
            {
                HIDRead(HIDReadData.Device[HIDReadData.iDevice]);
            }
        }
        void    CheckHIDWrite()
        {
            if (HIDWriteData.State == false)
            {
                var nbrDevice   = FindDeviceNumber();
                HIDWriteData.Device = new HID_DEVICE[nbrDevice];
                FindKnownHIDDevices(ref HIDWriteData.Device);

                for (var Index = 0; Index < nbrDevice; Index++)
                {
                    if (HIDWriteData.VendorID != 0)
                    {
                        var Count = 0;

                        if (HIDWriteData.Device[Index].Attributes.VendorID == HIDWriteData.VendorID)
                        {
                            Count++;
                        }
                        if (HIDWriteData.Device[Index].Attributes.ProductID == HIDWriteData.ProductID)
                        {
                            Count++;
                        }
                        if (HIDWriteData.Device[Index].Caps.UsagePage == HIDWriteData.UsagePage)
                        {
                            Count++;
                        }
                        if (HIDWriteData.Device[Index].Caps.Usage == HIDWriteData.Usage)
                        {
                            Count++;
                        }

                        if (Count == 4)
                        {
                            HIDWriteData.iDevice = Index;
                            HIDWriteData.State   = true;

                            break;
                        }
                        else
                        {
                            HIDWriteData.State = false;
                        }
                    }
                }
            }
            else
            {
                //HIDWrite(HIDWriteData.Device[HIDWriteData.iDevice]);
                //HIDWriteData.State = false;
            }
        }

        int counter = 0;
        int counter2 = 0;
        int counter3 = 0;
        byte oldWheelValue = 0;

        void    HIDRead(HID_DEVICE HID_Device)
        {
            //ManufacturerName.Text = "Manufacturer: " + HID_Device.Manufacturer;
            //ProductName.Text      = "Product: "      + HID_Device.Product;
            //SerialNumber.Text     = "SerialNumber: " + HID_Device.SerialNumber.ToString();

            //
            // Read what the USB device has sent to the PC and store the result into HID_Report[]
            //
            var HID_Report = new Byte[HID_Device.Caps.InputReportByteLength];
            
            if (HID_Report.Length > 0)
            {
                var varA = 0U;
                ReadFile(HID_Device.Pointer, HID_Report, HID_Device.Caps.InputReportByteLength, ref varA, IntPtr.Zero);

                //Read_Output.Invoke(new Action(() => Read_Output.Clear()));

                if(HID_Report[1] == 0xFF)
                {
                    counter++;
                    if (counter == 5)
                    {
                        WriteData[0] = 65;
                        CheckHIDWrite();
                        HIDWrite(HID_Device);
                        SendKeys.SendWait("{PGUP}");
                        counter = 0;
                    }
                } 
                else if (HID_Report[1] == 0x01)
                {
                    counter--;
                    if (counter == -5) {
                        WriteData[0] = 65;
                        CheckHIDWrite();
                        HIDWrite(HID_Device);
                        SendKeys.SendWait("{PGDN}");
                        counter = 0;
                    }
                }
                else if (HID_Report[2] == 0xFF)
                {
                    counter2++;
                    if (counter2 == 5)
                    {
                        SendKeys.SendWait("D");
                        counter2 = 0;
                    }
                    //SendMessageW (this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_DOWN);
                }
                else if (HID_Report[2] == 0x01)
                {
                    //SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_UP);
                    counter2--;
                    if (counter2 == -5)
                    {
                        SendKeys.SendWait("U");
                        counter2 = 0;
                    }
                }
                else if (HID_Report[3] < oldWheelValue)
                {
                    oldWheelValue = HID_Report[3];
                    counter3--;
                    if (counter3 == -5)
                    {
                        SendKeys.SendWait("{UP}");
                        counter3 = 0;
                    }
                }
                else if (HID_Report[3] > oldWheelValue)
                {
                    oldWheelValue = HID_Report[3];
                    counter3++;
                    if (counter3 == 5)
                    {
                        SendKeys.SendWait("{DOWN}");
                        counter3 = 0;
                    }
                }
                else if (HID_Report[4] == 0x20)
                {
                    SendKeys.SendWait("{LEFT}");
                }
                else if (HID_Report[4] == 0x10)
                {
                    SendKeys.SendWait("{RIGHT}");
                }
                else if (HID_Report[4] == 0x40)
                {
                    SendKeys.SendWait("{ENTER}");
                }
            
                /*
                for (var Index = 0; Index < HID_Device.Caps.InputReportByteLength; Index++)
                {
                    Read_Output.Invoke(new Action(() => Read_Output.Text += HID_Report[Index].ToString("X2") + " - "));                    
                }
                */
                
            }
        }

        public void T8()
        {

        }

        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
            IntPtr wParam, IntPtr lParam);


        void HIDWrite(HID_DEVICE HID_Device)
        {
            //
            // Sent to the USB device what is stored in WriteData[]
            //
            var HID_Report = new Byte[HID_Device.Caps.OutputReportByteLength];

            if (HID_Report.Length > 0)
            {
                HID_Report[0] = HIDWriteData.ReportID;

                for (var Index = 0; Index < WriteData.Length; Index++)
                {
                    if (Index + 1 < HID_Report.Length)
                    {
                        // Start at 1, as the first byte must be zero for HID report
                        HID_Report[Index + 1] = WriteData[Index];
                    }
                }

                var varA = 0U;
                WriteFile(HID_Device.Pointer, HID_Report, HID_Device.Caps.OutputReportByteLength, ref varA, IntPtr.Zero);
            }
        }

        Int32   FindDeviceNumber()
        {
            var hidGuid        = new Guid();
            var deviceInfoData = new SP_DEVICE_INTERFACE_DATA();

            HidD_GetHidGuid(ref hidGuid);

            //
            // Open a handle to the plug and play dev node.
            //
            SetupDiDestroyDeviceInfoList(hardwareDeviceInfo);
            hardwareDeviceInfo    = SetupDiGetClassDevs(ref hidGuid, IntPtr.Zero, IntPtr.Zero, DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);
            deviceInfoData.cbSize = Marshal.SizeOf(typeof(SP_DEVICE_INTERFACE_DATA));

            var Index = 0;
            while (SetupDiEnumDeviceInterfaces(hardwareDeviceInfo, IntPtr.Zero, ref hidGuid, Index, ref deviceInfoData))
            {
                Index++;
            }

            return (Index);
        }
        Int32   FindKnownHIDDevices(ref HID_DEVICE[] HID_Devices)
        {
            var hidGuid                 = new Guid();
            var deviceInfoData          = new SP_DEVICE_INTERFACE_DATA();
            var functionClassDeviceData = new SP_DEVICE_INTERFACE_DETAIL_DATA();

            HidD_GetHidGuid(ref hidGuid);

            //
            // Open a handle to the plug and play dev node.
            //
            SetupDiDestroyDeviceInfoList(hardwareDeviceInfo);
            hardwareDeviceInfo    = SetupDiGetClassDevs(ref hidGuid, IntPtr.Zero, IntPtr.Zero, DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);
            deviceInfoData.cbSize = Marshal.SizeOf(typeof(SP_DEVICE_INTERFACE_DATA));

            var iHIDD = 0;
            while (SetupDiEnumDeviceInterfaces(hardwareDeviceInfo, IntPtr.Zero, ref hidGuid, iHIDD, ref deviceInfoData))
            {
                var RequiredLength = 0;

                //
                // Allocate a function class device data structure to receive the
                // goods about this particular device.
                //
                SetupDiGetDeviceInterfaceDetail(hardwareDeviceInfo, ref deviceInfoData, IntPtr.Zero, 0, ref RequiredLength, IntPtr.Zero);

                if (IntPtr.Size == 8)
                {
                    functionClassDeviceData.cbSize = 8;
                }
                else if (IntPtr.Size == 4)
                {
                    functionClassDeviceData.cbSize = 5;
                }

                //
                // Retrieve the information from Plug and Play.
                //
                SetupDiGetDeviceInterfaceDetail(hardwareDeviceInfo, ref deviceInfoData, ref functionClassDeviceData, RequiredLength, ref RequiredLength, IntPtr.Zero);

                //
                // Open device with just generic query abilities to begin with
                //
                OpenHIDDevice(functionClassDeviceData.DevicePath, ref HID_Devices, iHIDD);

                iHIDD++;
            }

            return iHIDD;
        }
        void    OpenHIDDevice(String DevicePath, ref HID_DEVICE[] HID_Device, Int32 iHIDD)
        {
            //
            // RoutineDescription:
            // Given the HardwareDeviceInfo, representing a handle to the plug and
            // play information, and deviceInfoData, representing a specific hid device,
            // open that device and fill in all the relivant information in the given
            // HID_DEVICE structure.
            //
            HID_Device[iHIDD].DevicePath = DevicePath;

            //
            // The hid.dll api's do not pass the overlapped structure into deviceiocontrol
            // so to use them we must have a non overlapped device.  If the request is for
            // an overlapped device we will close the device below and get a handle to an
            // overlapped device
            //
            CloseHandle(HID_Device[iHIDD].Pointer);
            HID_Device[iHIDD].Pointer    = CreateFile(HID_Device[iHIDD].DevicePath, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, IntPtr.Zero);
            HID_Device[iHIDD].Caps       = new HIDP_CAPS();
            HID_Device[iHIDD].Attributes = new HIDD_ATTRIBUTES();

            //
            // If the device was not opened as overlapped, then fill in the rest of the
            // HID_Device structure.  However, if opened as overlapped, this handle cannot
            // be used in the calls to the HidD_ exported functions since each of these
            // functions does synchronous I/O.
            //
            HidD_FreePreparsedData(ref HID_Device[iHIDD].Ppd);
            HID_Device[iHIDD].Ppd = IntPtr.Zero;

            HidD_GetPreparsedData(HID_Device[iHIDD].Pointer, ref HID_Device[iHIDD].Ppd);
            HidD_GetAttributes(HID_Device[iHIDD].Pointer, ref HID_Device[iHIDD].Attributes);
            HidP_GetCaps(HID_Device[iHIDD].Ppd, ref HID_Device[iHIDD].Caps);
            
            var Buffer = Marshal.AllocHGlobal(126);
            {
                if (HidD_GetManufacturerString(HID_Device[iHIDD].Pointer, Buffer, 126))
                {
                    HID_Device[iHIDD].Manufacturer = Marshal.PtrToStringAuto(Buffer);
                }
                if (HidD_GetProductString(HID_Device[iHIDD].Pointer, Buffer, 126))
                {
                    HID_Device[iHIDD].Product = Marshal.PtrToStringAuto(Buffer);
                }
                if (HidD_GetSerialNumberString(HID_Device[iHIDD].Pointer, Buffer, 126))
                {
                    Int32.TryParse(Marshal.PtrToStringAuto(Buffer), out HID_Device[iHIDD].SerialNumber);
                }
            }
            Marshal.FreeHGlobal(Buffer);

            //
            // At this point the client has a choice.  It may chose to look at the
            // Usage and Page of the top level collection found in the HIDP_CAPS
            // structure.  In this way --------*it could just use the usages it knows about.
            // If either HidP_GetUsages or HidP_GetUsageValue return an error then
            // that particular usage does not exist in the report.
            // This is most likely the preferred method as the application can only
            // use usages of which it already knows.
            // In this case the app need not even call GetButtonCaps or GetValueCaps.
            //
            // In this example, however, wSendHID_PIDe will call FillDeviceInfo to look for all
            //    of the usages in the device.
            //
            //FillDeviceInfo(ref HID_Device, iHIDD);
        }
        void    FillDeviceInfo(ref HID_DEVICE[] HID_Device, Int32 iHIDD)
        {
            HIDP_BUTTON_CAPS[]  buttonCaps;
            HIDP_VALUE_CAPS[]   valueCaps;
            HID_DATA[]          data;
            Int32               Index, numValues;
            UInt16              numCaps, usage;

            //
            // setup Input Data buffers.
            //

            //
            // Allocate memory to hold on input report
            //
            HID_Device[iHIDD].InputReportBuffer = new IntPtr[HID_Device[iHIDD].Caps.InputReportByteLength];

            //HID_Device.InputReportBuffer = Marshal.AllocHGlobal();

            //
            // Allocate memory to hold the button and value capabilities.
            // NumberXXCaps is in terms of array elements.
            //
            HID_Device[iHIDD].InputButtonCaps = buttonCaps = new HIDP_BUTTON_CAPS[HID_Device[iHIDD].Caps.NumberInputButtonCaps];
            HID_Device[iHIDD].InputValueCaps  = valueCaps  = new HIDP_VALUE_CAPS[HID_Device[iHIDD].Caps.NumberInputValueCaps];

            //
            // Have the HidP_X functions fill in the capability structure arrays.
            //
            numCaps = HID_Device[iHIDD].Caps.NumberInputButtonCaps;
            if (numCaps > 0)
            {
                HidP_GetButtonCaps(HIDP_REPORT_TYPE.HidP_Input, buttonCaps, ref numCaps, HID_Device[iHIDD].Ppd);
            }

            numCaps = HID_Device[iHIDD].Caps.NumberInputValueCaps;
            if (numCaps > 0)
            {
                HidP_GetValueCaps(HIDP_REPORT_TYPE.HidP_Input, valueCaps, ref numCaps, HID_Device[iHIDD].Ppd);
            }

            //
            // Depending on the device, some value caps structures may represent more
            // than one value.  (A range).  In the interest of being verbose, over
            // efficient, we will expand these so that we have one and only one
            // struct _HID_DATA for each value.
            //
            // To do this we need to count up the total number of values are listed
            // in the value caps structure.  For each element in the array we test
            // for range if it is a range then UsageMax and UsageMin describe the
            // usages for this range INCLUSIVE.
            //
            numValues = 0;
            for (Index = 0; Index < HID_Device[iHIDD].Caps.NumberInputValueCaps; Index++)
            {
                if (valueCaps[Index].IsRange)
                {
                    numValues += valueCaps[Index].Range.UsageMax - valueCaps[Index].Range.UsageMin + 1;
                }
                else
                {
                    numValues++;
                }

            }

            valueCaps = HID_Device[iHIDD].InputValueCaps;

            //
            // Allocate a buffer to hold the struct _HID_DATA structures.
            // One element for each set of buttons, and one element for each value
            // found.
            //
            HID_Device[iHIDD].InputDataLength = HID_Device[iHIDD].Caps.NumberInputButtonCaps + numValues;
            HID_Device[iHIDD].InputData = data = new HID_DATA[HID_Device[iHIDD].InputDataLength];

            //
            // Fill in the button data
            //
            for (Index = 0; Index < HID_Device[iHIDD].Caps.NumberInputButtonCaps; Index++) 
            {
                data[Index].IsButtonData = true;
                data[Index].Status       = HIDP_STATUS_SUCCESS;
                data[Index].UsagePage    = buttonCaps[Index].UsagePage;

                if (buttonCaps[Index].IsRange) 
                {
                    data[Index].ButtonData.UsageMin = buttonCaps[Index].Range.UsageMin;
                    data[Index].ButtonData.UsageMax = buttonCaps[Index].Range.UsageMax;
                }
                else
                {
                    data[Index].ButtonData.UsageMin = data[Index].ButtonData.UsageMax = buttonCaps[Index].NotRange.Usage;
                }
        
                data[Index].ButtonData.MaxUsageLength = HidP_MaxUsageListLength(HIDP_REPORT_TYPE.HidP_Input, buttonCaps[Index].UsagePage, HID_Device[iHIDD].Ppd);
                //data[Index].ButtonData.Usages = new Int32[data[Index].ButtonData.MaxUsageLength];

                data[Index].ReportID = buttonCaps[Index].ReportID;
            }

            //
            // Fill in the value data
            //
            for (Index = 0; Index < HID_Device[iHIDD].Caps.NumberInputValueCaps; Index++)
            {
                if (valueCaps[Index].IsRange)
                {
                    // Never reach
                    for (usage = valueCaps[Index].Range.UsageMin; usage <= valueCaps[Index].Range.UsageMax; usage++)
                    {
                        data[Index].IsButtonData = false;
                        data[Index].Status = HIDP_STATUS_SUCCESS;
                        data[Index].UsagePage = valueCaps[Index].UsagePage;
                        data[Index].ValueData.Usage = usage;
                        data[Index].ReportID = valueCaps[Index].ReportID;
                    }
                }
                else
                {
                    data[Index].IsButtonData = false;
                    data[Index].Status = HIDP_STATUS_SUCCESS;
                    data[Index].UsagePage = valueCaps[Index].UsagePage;
                    data[Index].ValueData.Usage = valueCaps[Index].NotRange.Usage;
                    data[Index].ReportID = valueCaps[Index].ReportID;
                }
            }

            //
            // setup Output Data buffers.
            //
            HID_Device[iHIDD].OutputReportBuffer = new IntPtr[HID_Device[iHIDD].Caps.OutputReportByteLength];
            HID_Device[iHIDD].OutputButtonCaps   = buttonCaps = new HIDP_BUTTON_CAPS[HID_Device[iHIDD].Caps.NumberOutputButtonCaps];
            HID_Device[iHIDD].OutputValueCaps    = valueCaps  = new HIDP_VALUE_CAPS[HID_Device[iHIDD].Caps.NumberOutputValueCaps];

            numCaps = HID_Device[iHIDD].Caps.NumberOutputButtonCaps;

            if (numCaps > 0)
            {
                HidP_GetButtonCaps(HIDP_REPORT_TYPE.HidP_Output, buttonCaps, ref numCaps, HID_Device[iHIDD].Ppd);
            }

            numCaps = HID_Device[iHIDD].Caps.NumberOutputValueCaps;

            if (numCaps > 0)
            {
                HidP_GetValueCaps(HIDP_REPORT_TYPE.HidP_Output, valueCaps, ref numCaps, HID_Device[iHIDD].Ppd);
            }

            numValues = 0;
            for (Index = 0; Index < HID_Device[iHIDD].Caps.NumberOutputValueCaps; Index++)
            {
                if (valueCaps[Index].IsRange)
                {
                    numValues += valueCaps[Index].Range.UsageMax - valueCaps[Index].Range.UsageMin + 1;
                }
                else
                {
                    numValues++;
                }
            }

            valueCaps = HID_Device[iHIDD].OutputValueCaps;

            HID_Device[iHIDD].OutputDataLength = HID_Device[iHIDD].Caps.NumberOutputButtonCaps + numValues;
            HID_Device[iHIDD].OutputData = data = new HID_DATA[HID_Device[iHIDD].OutputDataLength];

            for (Index = 0; Index < HID_Device[iHIDD].Caps.NumberOutputButtonCaps; Index++)
            {
                data[Index].IsButtonData = true;
                data[Index].Status = HIDP_STATUS_SUCCESS;
                data[Index].UsagePage = buttonCaps[Index].UsagePage;

                if (buttonCaps[Index].IsRange)
                {
                    data[Index].ButtonData.UsageMin = buttonCaps[Index].Range.UsageMin;
                    data[Index].ButtonData.UsageMax = buttonCaps[Index].Range.UsageMax;
                }
                else
                {
                    data[Index].ButtonData.UsageMin = data[Index].ButtonData.UsageMax = buttonCaps[Index].NotRange.Usage;
                }

                data[Index].ButtonData.MaxUsageLength = HidP_MaxUsageListLength(HIDP_REPORT_TYPE.HidP_Output, buttonCaps[Index].UsagePage, HID_Device[iHIDD].Ppd);
                //data[Index].ButtonData.Usages = new short[data[Index].ButtonData.MaxUsageLength];
                data[Index].ReportID = buttonCaps[Index].ReportID;
            }

            for (Index = 0; Index < HID_Device[iHIDD].Caps.NumberOutputValueCaps; Index++)
            {
                if (valueCaps[Index].IsRange)
                {
                    // Never reach
                    for (usage = valueCaps[Index].Range.UsageMin; usage <= valueCaps[Index].Range.UsageMax; usage++)
                    {
                        data[Index].IsButtonData = false;
                        data[Index].Status = HIDP_STATUS_SUCCESS;
                        data[Index].UsagePage = valueCaps[Index].UsagePage;
                        data[Index].ValueData.Usage = usage;
                        data[Index].ReportID = valueCaps[Index].ReportID;
                    }
                }
                else
                {
                    data[Index].IsButtonData = false;
                    data[Index].Status = HIDP_STATUS_SUCCESS;
                    data[Index].UsagePage = valueCaps[Index].UsagePage;
                    data[Index].ValueData.Usage = valueCaps[Index].NotRange.Usage;
                    data[Index].ReportID = valueCaps[Index].ReportID;
                }
            }

            //
            // setup Feature Data buffers.
            //
            HID_Device[iHIDD].FeatureReportBuffer              = new IntPtr[HID_Device[iHIDD].Caps.FeatureReportByteLength];
            HID_Device[iHIDD].FeatureButtonCaps   = buttonCaps = new HIDP_BUTTON_CAPS[HID_Device[iHIDD].Caps.NumberFeatureButtonCaps];
            HID_Device[iHIDD].FeatureValueCaps    = valueCaps  = new HIDP_VALUE_CAPS[HID_Device[iHIDD].Caps.NumberFeatureValueCaps];

            numCaps = HID_Device[iHIDD].Caps.NumberFeatureButtonCaps;

            if (numCaps > 0)
            {
                HidP_GetButtonCaps(HIDP_REPORT_TYPE.HidP_Feature, buttonCaps, ref numCaps, HID_Device[iHIDD].Ppd);
            }

            numCaps = HID_Device[iHIDD].Caps.NumberFeatureValueCaps;

            if (numCaps > 0)
            {
                HidP_GetValueCaps(HIDP_REPORT_TYPE.HidP_Feature, valueCaps, ref numCaps, HID_Device[iHIDD].Ppd);
            }


            numValues = 0;
            for (Index = 0; Index < HID_Device[iHIDD].Caps.NumberFeatureValueCaps; Index++)
            {
                if (valueCaps[Index].IsRange)
                {
                    numValues += valueCaps[Index].Range.UsageMax - valueCaps[Index].Range.UsageMin + 1;
                }
                else
                {
                    numValues++;
                }
            }

            valueCaps = HID_Device[iHIDD].FeatureValueCaps;

            HID_Device[iHIDD].FeatureDataLength = HID_Device[iHIDD].Caps.NumberFeatureButtonCaps + numValues;
            HID_Device[iHIDD].FeatureData = data = new HID_DATA[HID_Device[iHIDD].FeatureDataLength];

            for (Index = 0; Index < HID_Device[iHIDD].Caps.NumberFeatureButtonCaps; Index++)
            {
                data[Index].IsButtonData = true;
                data[Index].Status = HIDP_STATUS_SUCCESS;
                data[Index].UsagePage = buttonCaps[Index].UsagePage;

                if (buttonCaps[Index].IsRange)
                {
                    data[Index].ButtonData.UsageMin = buttonCaps[Index].Range.UsageMin;
                    data[Index].ButtonData.UsageMax = buttonCaps[Index].Range.UsageMax;
                }
                else
                {
                    data[Index].ButtonData.UsageMin = data[Index].ButtonData.UsageMax = buttonCaps[Index].NotRange.Usage;
                }

                data[Index].ButtonData.MaxUsageLength = HidP_MaxUsageListLength(HIDP_REPORT_TYPE.HidP_Feature, buttonCaps[Index].UsagePage, HID_Device[iHIDD].Ppd);
                //data[Index].ButtonData.Usages = new short[data[Index].ButtonData.MaxUsageLength];

                data[Index].ReportID = buttonCaps[Index].ReportID;
            }

            for (Index = 0; Index < HID_Device[iHIDD].Caps.NumberFeatureValueCaps; Index++)
            {
                if (valueCaps[Index].IsRange)
                {
                    // Never reach
                    for (usage = valueCaps[Index].Range.UsageMin; usage <= valueCaps[Index].Range.UsageMax; usage++)
                    {
                        data[Index].IsButtonData = false;
                        data[Index].Status = HIDP_STATUS_SUCCESS;
                        data[Index].UsagePage = valueCaps[Index].UsagePage;
                        data[Index].ValueData.Usage = usage;
                        data[Index].ReportID = valueCaps[Index].ReportID;
                    }
                }
                else
                {
                    data[Index].IsButtonData = false;
                    data[Index].Status = HIDP_STATUS_SUCCESS;
                    data[Index].UsagePage = valueCaps[Index].UsagePage;
                    data[Index].ValueData.Usage = valueCaps[Index].NotRange.Usage;
                    data[Index].ReportID = valueCaps[Index].ReportID;
                }
            }
        }
        void    PackReport(IntPtr ReportBuffer, UInt16 ReportBufferLength, HIDP_REPORT_TYPE ReportType, HID_DATA[] Data, Int32 DataLength, IntPtr Ppd)
        {
            // /*++
            // Routine Description:
            //    This routine takes in a list of HID_DATA structures (DATA) and builds 
            //       in ReportBuffer the given report for all data values in the list that 
            //       correspond to the report ID of the first item in the list.  
            // 
            //    For every data structure in the list that has the same report ID as the first
            //       item in the list will be set in the report.  Every data item that is 
            //       set will also have it's IsDataSet field marked with TRUE.
            // 
            //    A return value of FALSE indicates an unexpected error occurred when setting
            //       a given data value.  The caller should expect that assume that no values
            //       within the given data structure were set.
            // 
            //    A return value of TRUE indicates that all data values for the given report
            //       ID were set without error.
            // --

            Int32   numUsages; // Number of usages to set for a given report.
            Int32   Index;
            Int32   CurrReportID;

            //
            // Go through the data structures and set all the values that correspond to
            //   the CurrReportID which is obtained from the first data structure 
            //   in the list
            //
            CurrReportID = Data[0].ReportID;

            for (Index = 0; Index < DataLength; Index++) 
            {
                //
                // There are two different ways to determine if we set the current data
                //    structure: 
                //    1) Store the report ID were using and only attempt to set those
                //        data structures that correspond to the given report ID.  This
                //        example shows this implementation.
                //
                //    2) Attempt to set all of the data structures and look for the 
                //        returned status value of HIDP_STATUS_INVALID_REPORT_ID.  This 
                //        error code indicates that the given usage exists but has a 
                //        different report ID than the report ID in the current report 
                //        buffer
                //
                if (Data[Index].ReportID == CurrReportID) 
                {
                    if (Data[Index].IsButtonData) 
                    {
                        numUsages = Data[Index].ButtonData.MaxUsageLength;
                        Data[Index].Status = HidP_SetUsages(ReportType,
                                                       Data[Index].UsagePage,
                                                       0,
                                                       Data[Index].ButtonData.Usages,
                                                       ref numUsages,
                                                       Ppd,
                                                       ReportBuffer,
                                                       ReportBufferLength);
                    }
                    else
                    {
                        Data[Index].Status = HidP_SetUsageValue(ReportType,
                                                           Data[Index].UsagePage,
                                                           0,
                                                           Data[Index].ValueData.Usage,
                                                           Data[Index].ValueData.Value,
                                                           Ppd,
                                                           ReportBuffer,
                                                           ReportBufferLength);
                    }
                }
            }

            //
            // At this point, all data structures that have the same ReportID as the
            //    first one will have been set in the given report.  Time to loop 
            //    through the structure again and mark all of those data structures as
            //    having been set.
            //
            for (Index = 0; Index < DataLength; Index++) 
            {
                if (CurrReportID == Data[Index].ReportID)
                {
                    Data[Index].IsDataSet = true;
                }
            }
        }

        private void Read_Byte0_Input(object sender, EventArgs e)
        {
            Byte.TryParse(Byte0_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out WriteData[0]);
        }
        private void Read_Byte1_Input(object sender, EventArgs e)
        {
            Byte.TryParse(Byte1_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out WriteData[1]);

        }
        private void Read_Byte2_Input(object sender, EventArgs e)
        {
            Byte.TryParse(Byte2_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out WriteData[2]);
        }
        private void Read_Byte3_Input(object sender, EventArgs e)
        {
            Byte.TryParse(Byte3_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out WriteData[3]);
        }
        private void Read_Byte4_Input(object sender, EventArgs e)
        {
            Byte.TryParse(Byte4_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out WriteData[4]);
        }
        private void Read_Byte5_Input(object sender, EventArgs e)
        {
            Byte.TryParse(Byte5_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out WriteData[5]);
        }
        private void Read_Byte6_Input(object sender, EventArgs e)
        {
            Byte.TryParse(Byte6_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out WriteData[6]);
        }

        private void HIDRead_Button_Click(object sender, EventArgs e)
        {
            UInt16.TryParse(ReadHID_VID_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDReadData.VendorID);
            UInt16.TryParse(ReadHID_PID_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDReadData.ProductID);

            HIDReadData.State = false;
        }
        private void HIDWrite_Button_Click(object sender, EventArgs e)
        {
            UInt16.TryParse(SendHID_VID_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDWriteData.VendorID);
            UInt16.TryParse(SendHID_PID_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDWriteData.ProductID);
            UInt16.TryParse(SendHID_UsagePage_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDWriteData.UsagePage);
            UInt16.TryParse(SendHID_Usage_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDWriteData.Usage);
            Byte.TryParse(SendHID_RID_Input.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out HIDWriteData.ReportID);

            HIDWriteData.State = false;
            CheckHIDWrite();
            HIDWrite(HIDWriteData.Device[HIDWriteData.iDevice]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteData[0] = 64;
            HIDWrite(HIDWriteData.Device[HIDWriteData.iDevice]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WriteData[0] = 128;
            HIDWrite(HIDWriteData.Device[HIDWriteData.iDevice]);
        }
        //==========================================================================================================================================================================================================
        // END_FUNCTION      END_FUNCTION      END_FUNCTION      END_FUNCTION      END_FUNCTION      END_FUNCTION      END_FUNCTION      END_FUNCTION      END_FUNCTION      END_FUNCTION      END_FUNCTION
        //==========================================================================================================================================================================================================
    }
}