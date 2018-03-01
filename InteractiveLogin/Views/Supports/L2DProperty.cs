using L2DLib.Framework;

namespace InteractiveLogin.Views
{
    public class L2DProperty
    {
        #region 변수
        private L2DModel _model;
        private string _key;
        private float _value;
        private float _currentValue;
        #endregion

        #region 속성
        public L2DModel Model => _model;

        public string Key => _key;

        public float Variable { get; set; }

        public float AnimatableValue
        {
            get => _value;
            set
            {
                _value = value;
                Update();
            }
        }
        #endregion

        #region 생성자
        public L2DProperty(L2DModel model, string key, float variable = 30.0f)
        {
            Variable = variable;
            _model = model;
            _key = key;

            Update();
            _value = _currentValue;
        }
        #endregion

        #region 내부 함수
        internal void Update()
        {
            _currentValue = _model.GetParamFloat(_key);
            _model.AddToParamFloat(_key, (_value - _currentValue) / Variable);
        }
        #endregion
    }
}
