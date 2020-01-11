using SharpDX.XInput;
using System.Threading;
using WindowsInput;

namespace TheMatiaz0_MonobeamTheMercenary.Input
{
    public class Gamepad
    {
        private const int MovementDivider = 2_000;
        private const int ScrollDivider = 10_000;
        private const int RefreshRate = 60;

        private Controller _controller;
        private IMouseSimulator _mouseSimulator;
        private Timer _timer;

        private bool _wasADown;
        private bool _wasBDown;
        private bool _wasR2Down;
        private bool _wasXDown;
        private bool isR2Down;

        public Gamepad()
        {
            _controller = new Controller(UserIndex.One);
            _mouseSimulator = new InputSimulator().Mouse;

            if (_controller.IsConnected)
            {
                _timer = new Timer(obj => Update());
            }

        }

        public void Start()
        {
            if (_controller.IsConnected)
            {
                _timer.Change(0, 1000 / RefreshRate);
            }

        }

        private void Update()
        {
            _controller.GetState(out var state);

            Movement(state);
            Scroll(state);
            Choice1(state);
            Choice2(state);
            Choice3(state);
            Choice4(state);
            EnterConfirm(state);

        }

        private void Movement(State state)
        {
            int x = state.Gamepad.LeftThumbX / MovementDivider;
            int y = state.Gamepad.LeftThumbY / MovementDivider;
            _mouseSimulator.MoveMouseBy(x, -y);
        }

        private void EnterConfirm(State state)
        {
            byte pressure = state.Gamepad.RightTrigger;

            if (pressure > 254)
            {
                isR2Down = true;
            }

            if (pressure < 254)
            {
                isR2Down = false;
            }

            if (isR2Down && !_wasR2Down)
            {
                _mouseSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.RETURN);
            }

            if (!isR2Down && _wasR2Down)
            {
                _mouseSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.RETURN);
            }

            _wasR2Down = isR2Down;
        }

        private void Scroll(State state)
        {
            var x = state.Gamepad.RightThumbX / ScrollDivider;
            var y = state.Gamepad.RightThumbY / ScrollDivider;

            _mouseSimulator.HorizontalScroll(x);
            _mouseSimulator.VerticalScroll(y);
        }

        private void Choice1(State state)
        {
            bool isYDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Y);

            if (isYDown && !_wasADown)
            {
                _mouseSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.BACK);
                _mouseSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.NUMPAD1);
            }

            if (!isYDown && _wasADown)
            {
                _mouseSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.BACK);
                _mouseSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.NUMPAD1);
            }

            _wasADown = isYDown;
        }

        private void Choice2(State state)
        {
            bool isBDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B);

            if (isBDown && !_wasBDown)
            {
                _mouseSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.BACK);
                _mouseSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.NUMPAD2);
            }

            if (!isBDown && _wasBDown)
            {
                _mouseSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.BACK);
                _mouseSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.NUMPAD2);
            }

            _wasBDown = isBDown;
        }

        private void Choice3(State state)
        {
            bool isADown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A);

            if (isADown && !_wasADown)
            {
                _mouseSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.BACK);
                _mouseSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.NUMPAD3);
            }

            if (!isADown && _wasADown)
            {
                _mouseSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.BACK);
                _mouseSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.NUMPAD3);
            }

            _wasADown = isADown;
        }

        private void Choice4(State state)
        {
            bool isXDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.X);

            if (isXDown && !_wasXDown)
            {
                _mouseSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.BACK);
                _mouseSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.NUMPAD4);
            }

            if (!isXDown && _wasXDown)
            {
                _mouseSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.BACK);
                _mouseSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.NUMPAD4);
            }

            _wasXDown = isXDown;

        }
    }
}
