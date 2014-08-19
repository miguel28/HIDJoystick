using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoytickInterop
{

    public enum JoystickHatPositions
    {
	    HAT_CENTERED,
	    HAT_UP,
	    HAT_RIGHT,
	    HAT_DOWN,
	    HAT_LEFT,
	    HAT_RIGHTUP,
	    HAT_RIGHTDOWN,
	    HAT_LEFTUP,
	    HAT_LEFTDOWN
    };
    /* OSX Positions
    public enum JoystickHatPositions
    {
        HAT_CENTERED,
        HAT_UP,
        HAT_RIGHT,
        HAT_LEFT2,
        HAT_DOWN,
        HAT_RIGHTUP,
        HAT_RIGHTDOWN,
        HAT_LEFTUP,
        HAT_LEFT
    };
    */

    public class SDLJoystick
    {
		#region Native Metods

#if WIN64
        private const string DLL_FILE_NAME = "SDL64.dll";
#else
        private const string DLL_FILE_NAME = "SDL.dll";
#endif

        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SDL_Init(int Flags);

        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
        private extern static int SDL_InitSubSystem(uint Flags);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi)]
        private extern static string SDL_GetError();

        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
		private extern static int SDL_NumJoysticks();

        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
        private extern static int SDL_JoystickNumButtons(IntPtr joystick);

        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
        private extern static int SDL_JoystickNumAxes(IntPtr joystick);

        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
        private extern static int SDL_JoystickNumBalls(IntPtr joystick);

        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
        private extern static int SDL_JoystickNumHats(IntPtr joystick);

        //////
        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.ThisCall)]
        private extern static IntPtr SDL_JoystickName(int joystick);

        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
		private extern static void SDL_JoystickEventState(int Enable);

        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
		private extern static IntPtr SDL_JoystickOpen(int numJoystick);

        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
        private extern static void SDL_JoystickClose(IntPtr joystick);

        [DllImport(DLL_FILE_NAME)]
		private extern static void SDL_JoystickUpdate();

        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
		private extern static byte SDL_JoystickGetButton(IntPtr joystick, int button);

        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
		private extern static short SDL_JoystickGetAxis(IntPtr joystick, int Axis);

        [DllImport(DLL_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
        private extern static byte SDL_JoystickGetHat(IntPtr joystick, int hat);

		#endregion

        #region Private Methods
		private int NumButtons;
        private int NumAxis;
        private int NumHats;
        private int NumBalls;
        private bool Open = false;
        private string joyName;
        private IntPtr JoyHandle = IntPtr.Zero;
        private UInt16[] mButtonHeld;
        private UInt16[] mButtonNewpress;
        private UInt16[] bButtonNewpress;
        private UInt16[] hatPrevState;
	    private UInt16[] hatNewpressState;

        private static bool Initialized = false;
        private static int NumJoysticks = 0;
        private static List<string> _JoystickNames = new List<string>();
        private static string CastPtrToString(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }
        #endregion

        #region Properties
        public int Buttons
        {
            get
            {
                return NumButtons;
            }
        }
        public int Balls
        {
            get
            {
                return NumBalls;
            }
        }
        public int Hats
        {
            get
            {
                return NumHats;
            }
        }
        public int Axes
        {
            get
            {
                return NumAxis;
            }
        }
        public bool IsOpen
        {
            get
            {
                return Open;
            }
        }
        public string Name
        {
            get
            {
                return joyName;
            }
        }
        #endregion

        #region Static Members
        public static int Joysticks
        {
            get
            {
                return NumJoysticks; 
            }
        }
        public static bool IsInitialized
        {
            get
            {
                return Initialized;
            }
        }
        public static List<string> JoystickNames
        {
            get
            {
                return _JoystickNames;
            }
        }
        public static void InitJoystickSystem()
        {
            if (Initialized)
                return;

            try
            {
                if (SDL_Init((int)0x00000200) < 0)
                {
                    MessageBox.Show(SDL_GetError(),"Couldn't initialize SDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Initialized = false;
                }
                else
                {
                    SDL_JoystickEventState(1);

                    NumJoysticks = SDL_NumJoysticks();
                    //Console.WriteLine(NumJoysticks.ToString() + " joysticks were found.");
                    int i;
                    for (i = 0; i < NumJoysticks; i++)
                    {
                        string name = CastPtrToString(SDL_JoystickName(i));
                        _JoystickNames.Add(name);
                        //Console.WriteLine("  " + i.ToString() + ".-" + name);
                    }
                    Initialized = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Couldn't initialize SDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        
        #region private Members   
        private int GetJoystickNumberByName(string name)
        {
            int ret = 0;

            foreach (string current in _JoystickNames)
            {
                if (current.Contains(name) && name.Length == current.Length)
                    return ret;
                ret++;
            }
            return -1;
        }
        private void InitJoystick(int numJoystick)
        {
            if (!Initialized)
                return;

            JoyHandle = SDL_JoystickOpen(numJoystick);

            if (JoyHandle != IntPtr.Zero)
                Open = true;
            else MessageBox.Show("Couldn't initialize SDL Joystick", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void GetJoystickInfo()
        {
            NumButtons = SDL_JoystickNumButtons(JoyHandle);
            NumBalls = SDL_JoystickNumBalls(JoyHandle);
            NumHats = SDL_JoystickNumHats(JoyHandle);
            NumAxis = SDL_JoystickNumAxes(JoyHandle);
            
            InitControls();
        }
        private void InitControls()
        {
            mButtonHeld = new UInt16[NumButtons];
            mButtonNewpress = new UInt16[NumButtons];
            bButtonNewpress = new UInt16[NumButtons];

            hatPrevState = new UInt16[5];
            hatNewpressState = new UInt16[5];
        }
        private void JoystickEvent()
        {
            SDL_JoystickUpdate();

            int i;
            for (i = 0; i < NumButtons; i++)
            {
                mButtonHeld[i] = SDL_JoystickGetButton(JoyHandle, i);
            }
        }
        private byte _GetHatHeld(int NumHat)
        {
            return SDL_JoystickGetHat(JoyHandle, NumHat);
        }
        #endregion

        #region Contructors
        public SDLJoystick(int numJoystick)
		{
            if (!Initialized)
                InitJoystickSystem();

            InitJoystick(numJoystick);
            joyName = CastPtrToString(SDL_JoystickName(numJoystick));
            GetJoystickInfo();

            Update();
		}
        public SDLJoystick(string strName)
        {
            if (!Initialized)
                InitJoystickSystem();

            int numJoystick = GetJoystickNumberByName(strName);
            joyName = CastPtrToString(SDL_JoystickName(numJoystick));
            InitJoystick(numJoystick);
            GetJoystickInfo();

            Update();
        }
        #endregion

        #region Public Members
        public void Update()
		{
            if (!Open)
                return;

            JoystickEvent();
			int i;
			for(i=0; i<NumButtons; i++)
			{
				if(mButtonHeld[i]==1)
				{
					if(bButtonNewpress[i]==1){
						mButtonNewpress[i] = 0;
						bButtonNewpress[i] = 1;
					}
					else 
					{
						mButtonNewpress[i] = 1;
						bButtonNewpress[i] = 1;
					}
				}
				else
				{
					bButtonNewpress[i] = 0;
					mButtonNewpress[i] = 0;
				}
			}

            /// Determine Hat Status

            byte TempHat = _GetHatHeld(0);
            if (hatPrevState[0] != TempHat && TempHat != (byte)JoystickHatPositions.HAT_CENTERED)
            {
                hatNewpressState[0] = TempHat;
            }
            else
            {
                hatNewpressState[0] = (byte)JoystickHatPositions.HAT_CENTERED;
            }
            hatPrevState[0] = TempHat;

            TempHat = _GetHatHeld(1);
            if (hatPrevState[1] != TempHat && TempHat != (byte)JoystickHatPositions.HAT_CENTERED)
            {
                hatNewpressState[1] = TempHat;
            }
            else
            {
                hatNewpressState[1] = (byte)JoystickHatPositions.HAT_CENTERED;
            }
            hatPrevState[1] = TempHat;
		}
        public float GetAxis(short Axis)
		{
            if (Axis > Axes)
                return 0.0f;

            float Value = (float)((float)SDL_JoystickGetAxis(JoyHandle, Axis)/32768.0f);

			try
			{
				if (Math.Abs (Value) > 0.15)
					return Value;
				else
					return 0.0f;
			}
			catch 
			{
				return 0.0f;
			}
		}
        public JoystickHatPositions GetHatHeld(int NumHat)
        {
            return (JoystickHatPositions)SDL_JoystickGetHat(JoyHandle, NumHat);
        }
        public JoystickHatPositions GetHatNewPress(int NumHat)
        {
	        return (JoystickHatPositions)hatNewpressState[0];
        }
		public bool ButtonHeld(short button)
		{
			if(button > NumButtons || button == 0)return false;
			else 
			{
				if (mButtonHeld [button - 1] == 1)
					return true;
				else
					return false;
			}
		}
		public bool ButtonNewpress(short button)
		{
			if (button > NumButtons || button == 0)
				return false;
			else 
			{
				if (mButtonNewpress [button - 1] == 1)
					return true;
				else
					return false;
			}
        }
        #endregion
    }
}
