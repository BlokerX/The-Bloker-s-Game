using UnityEngine;

public class TorchOnOff : MonoBehaviour
{
    public bool torchIsOn;

    //public AudioSource CampfireSound;
    public Light TorchLight;
    public ParticleSystem Fire;
    //public ParticleSystem Flames;
    //public ParticleSystem Glow;
    //public ParticleSystem SmokeDark;
    //public ParticleSystem SmokeLit;
    //public ParticleSystem SparksFalling;
    //public ParticleSystem SparksRising;

    public GUISkin HelpMessageSkin;

    public KeyCode LightOrPutOutTorch_KeyCode = KeyCode.F;

    private bool IsPlayerNear = false;

    private const string PlayerColliderTag = "PlayerCamera";

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        // Check is ON/OFF on start
        switch (torchIsOn)
        {
            // Burn
            case true:
                TorchLight.enabled = true;
                Fire.enableEmission = true;
                //Flames.enableEmission = true;
                //Glow.enableEmission = true;
                //SmokeDark.enableEmission = true;
                //SmokeLit.enableEmission = true;
                //SparksFalling.enableEmission = true;
                //SparksRising.enableEmission = true;

                torchIsOn = true;
                break;
        }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    [System.Obsolete]
    void OnTriggerStay(Collider collider)
    {
        // Dla objektu typu gracz:
        if (collider.CompareTag(PlayerColliderTag))
        {
            if (Input.GetKeyDown(LightOrPutOutTorch_KeyCode))
            {
                LightOrPutOutTorch();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(PlayerColliderTag))
        {
            IsPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag(PlayerColliderTag))
        {
            IsPlayerNear = false;
        }
    }

    [System.Obsolete]
    void LightOrPutOutTorch()
    {
        switch (torchIsOn)
        {
            // Light a fire
            case false:
                //CampfireSound.Play();
                TorchLight.enabled = true;
                Fire.enableEmission = true;
                //Flames.enableEmission = true;
                //Glow.enableEmission = true;
                //SmokeDark.enableEmission = true;
                //SmokeLit.enableEmission = true;
                //SparksFalling.enableEmission = true;
                //SparksRising.enableEmission = true;

                torchIsOn = true;
                break;

            // Put out the fire
            case true:
                //CampfireSound.Stop();
                TorchLight.enabled = false;
                Fire.enableEmission = false;
                //Flames.enableEmission = false;
                //Glow.enableEmission = false;
                //SmokeDark.enableEmission = false;
                //SmokeLit.enableEmission = false;
                //SparksFalling.enableEmission = false;
                //SparksRising.enableEmission = false;

                torchIsOn = false;
                break;
        }
    }

    private void OnGUI()
    {
        if (IsPlayerNear)
        {
            if (HelpMessageSkin != null) GUI.skin = HelpMessageSkin;
            Rect rect = new Rect(25, Screen.height - 25 - 110, 420, 110);
            switch (torchIsOn)
            {
                case false:
                    GUI.Box(rect, $"Press [{LightOrPutOutTorch_KeyCode.ToString()}]\n to light torch");
                    break;

                case true:
                    GUI.Box(rect, $"Press [{LightOrPutOutTorch_KeyCode.ToString()}]\n to put out torch");
                    break;
            }
        }
    }

    [System.Obsolete]
    private void Update()
    {
        if(this.gameObject.TryGetComponent<ItemObject>(out var itemObject) && itemObject.HaveOvner)
        {
            if (Input.GetKeyDown(LightOrPutOutTorch_KeyCode))
                LightOrPutOutTorch();
        }
    }

}

