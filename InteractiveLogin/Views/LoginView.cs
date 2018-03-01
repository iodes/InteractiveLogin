using L2DLib.Framework;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InteractiveLogin.Views
{
    public class LoginView : L2DView, ILoginView
    {
        #region 변수
        private L2DProperty _armL;
        private L2DProperty _armR;

        private L2DProperty _bodyX;
        private L2DProperty _bodyZ;

        private L2DProperty _angleX;
        private L2DProperty _angleY;
        private L2DProperty _angleZ;

        private L2DProperty _eyeX;
        private L2DProperty _eyeY;
        private L2DProperty _eyeKiraKira;

        private L2DProperty _mouthOpen;

        private bool _isSucceeded = false;
        private bool _isFailed = false;
        #endregion

        #region 속성
        public TextBox Target { get; set; }

        public bool IsPrepared { get; set; } = false;
        #endregion

        #region 내부 함수
        private bool Prepare()
        {
            if (!IsPrepared && Target != null && Model != null)
            {
                _armL = new L2DProperty(Model, "PARAM_ARM_L");
                _armR = new L2DProperty(Model, "PARAM_ARM_R");

                _bodyX = new L2DProperty(Model, "PARAM_BODY_X");
                _bodyZ = new L2DProperty(Model, "PARAM_BODY_Z");

                _angleX = new L2DProperty(Model, "PARAM_ANGLE_X");
                _angleY = new L2DProperty(Model, "PARAM_ANGLE_Y");
                _angleZ = new L2DProperty(Model, "PARAM_ANGLE_Z");

                _eyeX = new L2DProperty(Model, "PARAM_EYE_BALL_X");
                _eyeY = new L2DProperty(Model, "PARAM_EYE_BALL_Y");
                _eyeKiraKira = new L2DProperty(Model, "PARAM_EYE_BALL_KIRAKIRA");

                _mouthOpen = new L2DProperty(Model, "PARAM_MOUTH_OPEN_Y");

                IsPrepared = true;
            }

            return IsPrepared;
        }

        private Size MeasureText(TextBox target)
        {
            var formatted =
                new FormattedText(target.Text.Substring(0, target.CaretIndex),
                CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                new Typeface(target.FontFamily, target.FontStyle, target.FontWeight, target.FontStretch),
                target.FontSize, target.Foreground);

            return new Size(formatted.Width, formatted.Height);
        }
        #endregion

        #region 사용자 함수
        public void ShowSuccess()
        {
            Reset(true);
            _isSucceeded = true;
            Model.Motion["tap_body"].Where(x => x.Path.Contains("tapBody01")).First().StartMotion();
        }

        public void ShowFail()
        {
            Reset(true);
            _isFailed = true;
            Model.Motion["shake"].Where(x => x.Path.Contains("shake01")).First().StartMotion();
        }

        public void Reset(bool force = false)
        {
            if (!force)
            {
                Model.Motion["flick_head"].Where(x => x.Path.Contains("flickHead03")).First().StartMotion();
            }

            _isSucceeded = false;
            _isFailed = false;
        }
        #endregion

        public override void Rendering()
        {
            if (Prepare())
            {
                Model.LoadParam();

                var progress = (float)(MeasureText(Target).Width / (Target.ActualWidth / 2)) - 1.0f;

                _angleZ.AnimatableValue = 0.0f;
                _mouthOpen.AnimatableValue = 0.0f;

                if (Target.IsFocused)
                {
                    if (_isSucceeded || _isFailed)
                    {
                        Reset();
                    }

                    _armL.AnimatableValue = -1.0f;
                    _armR.AnimatableValue = -1.0f;

                    _bodyX.AnimatableValue = Math.Min(progress, 1.0f);
                    _bodyZ.AnimatableValue = Math.Min(progress, 1.0f) * 0.5f;

                    _angleX.AnimatableValue = Math.Min(progress * 30f, 30f);
                    _angleY.AnimatableValue = -20f;

                    _eyeX.AnimatableValue = Math.Min(progress, 1.0f);
                    _eyeY.AnimatableValue = -1.0f;
                    _eyeKiraKira.AnimatableValue = 1.0f;
                }
                else
                {
                    _armL.AnimatableValue = 0.0f;
                    _armR.AnimatableValue = 0.0f;

                    _bodyX.AnimatableValue = 0.0f;
                    _bodyZ.AnimatableValue = 0.0f;

                    _angleX.AnimatableValue = 0.0f;
                    _angleY.AnimatableValue = 0.0f;

                    _eyeX.AnimatableValue = 0.0f;
                    _eyeY.AnimatableValue = 0.0f;
                    _eyeKiraKira.AnimatableValue = 0.0f;
                }

                Model.SaveParam();
            }
        }
    }
}
