using UnityEngine;
using UnityEngine.UI;
using TMPro; // Eğer TextMeshPro kullanıyorsan
using Firebase.Auth;
using UnityEngine.EventSystems;

public class AuthUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject loginPanel;
    public GameObject signupPanel;

    [Header("Login Fields")]
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;
    public TMP_Text loginErrorText;

    [Header("Signup Fields")]
    public TMP_InputField signupEmailInput;
    public TMP_InputField signupPasswordInput;
    public TMP_InputField signupPasswordRepeatInput;
    public TMP_Text signupErrorText;

    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        ShowLogin();
    }

    public void ShowLogin()
    {
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
        loginErrorText.text = "";
    }

    public void ShowSignup()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
        signupErrorText.text = "";
    }

    public void OnLoginButton()
    {
        string email = loginEmailInput.text;
        string password = loginPasswordInput.text;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                loginErrorText.text = "Giriş başarısız: " + task.Exception?.Message;
            }
            else
            {
                // Başarılı giriş
                // Ana menüye veya oyuna geçiş
            }
        }, System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());
    }

    public void OnSignupButton()
    {
        string email = signupEmailInput.text;
        string password = signupPasswordInput.text;
        string passwordRepeat = signupPasswordRepeatInput.text;

        if (password != passwordRepeat)
        {
            signupErrorText.text = "Şifreler uyuşmuyor!";
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                signupErrorText.text = "Kayıt başarısız: " + task.Exception?.Message;
            }
            else
            {
                // Başarılı kayıt
                // İstersen otomatik giriş yap veya login paneline yönlendir
            }
        }, System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());
    }
}

public class GoToLoginText : MonoBehaviour, IPointerClickHandler
{
    public AuthUI authUI; // AuthUI scriptini buraya Inspector'dan sürükle

    private TMP_Text textMeshPro;

    void Awake()
    {
        textMeshPro = GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMeshPro, eventData.position, eventData.pressEventCamera);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo[linkIndex];
            if (linkInfo.GetLinkID() == "login")
            {
                authUI.ShowLogin();
            }
        }
    }
}

public class GoToSignupText : MonoBehaviour, IPointerClickHandler
{
    public AuthUI authUI; // AuthUI scriptini buraya Inspector'dan sürükle

    private TMP_Text textMeshPro;

    void Awake()
    {
        textMeshPro = GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Mouse pozisyonundan linke tıklanıp tıklanmadığını kontrol et
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMeshPro, eventData.position, eventData.pressEventCamera);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo[linkIndex];
            if (linkInfo.GetLinkID() == "signup")
            {
                authUI.ShowSignup();
            }
        }
    }
}