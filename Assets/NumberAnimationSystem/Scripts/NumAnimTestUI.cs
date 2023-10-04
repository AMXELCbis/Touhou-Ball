using UnityEngine;
using UnityEngine.UI;

public class NumAnimTestUI : MonoBehaviour
{
    public NumAnimController _jpController;
    public GameObject _newJpNum;
    public GameObject _newAnimationTime;
    private bool _zeroStart;

    public void Start()
    {
        GameObject.Find("StartFromZero").GetComponent<Toggle>().onValueChanged.AddListener(isOn => OnClick(isOn));
    }

    public void OnAddBtnDown()
    {
        if (_zeroStart)
        {
            _jpController.m_currentNum = 0;
        }
        
        NumAnimData d = ScriptableObject.CreateInstance<NumAnimData>();
        d._total = int.Parse(_newJpNum.GetComponent<Text>().text);
        d._animationTime = int.Parse(_newAnimationTime.GetComponent<Text>().text);
        _jpController.Animate(d);
    }

    public void OnClick(bool isOn)
    {
        if (isOn)
        {
            _zeroStart = true;
        }
        else
        {
            _zeroStart = false;
        }
    }

}
