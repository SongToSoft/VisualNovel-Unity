using UnityEngine;

public class VN_DialogModel : MonoBehaviour
{
    [SerializeField] private VN_DialogView m_dialogView;
    [SerializeField] private string m_defaultDialog = "dialog_0";

    private int m_replicaCount = 0;

    private VN_Dialog m_currentDialog;

    private static VN_DialogModel m_instance;

    public void Awake()
    {
        m_instance = this;
    }

    public void Start()
    {
        var profile = VN_SaveService.GetProfile();
        if (profile != null && profile.currentDialog != "")
        {
            LoadDialog(profile.currentDialog, profile.currentReplica - 1);
            VN_LocationModel.Instance().SetLocation(profile.currentLocationName);
        }
        else
        {
            LoadDialog(m_defaultDialog);
        }
    }

    public static VN_DialogModel Instance()
    {
        return m_instance;
    }

    public void SetTextDelay(float delay)
    {
        if (m_dialogView)
        {
            m_dialogView.SetTextDelay(delay);
        }
    }

    public string GetCurrentDialogId()
    {
        return m_currentDialog.id;
    }

    public int GetCurrentRepliceCount()
    {
        return m_replicaCount;
    }

    public void DialogDown()
    {
        if (m_dialogView)
        {
            m_dialogView.DialogDown();
        }
    }

    public void DialogUp()
    {
        if (m_dialogView)
        {
            m_dialogView.DialogUp();
        }
    }

    public void OnNextClick()
    {
        if (m_dialogView)
        {
            m_dialogView.OnNextClick();
        }
        NextReplica();
    }

    public void LoadDialog(string dialogId, int replicaCount = 0)
    {
        var dialogPath = Resources.Load<TextAsset>("Jsons/Dialogs/" + dialogId);
        m_currentDialog = JsonUtility.FromJson<VN_Dialog>(dialogPath.text);
        VN_Logger.Log("[VN_DialogModel][LoadDialog] Load New Dialog: " + dialogId + ", replicas count: " + m_currentDialog.replicas.Count);
        m_replicaCount = replicaCount;

        if (m_dialogView)
        {
            m_dialogView.LoadDialog();
        }
        VN_LocationModel.Instance().SetLocation(m_currentDialog.location);
        NextReplica();
    }

    public void ClearDialog()
    {
        if (m_dialogView)
        {
            m_dialogView.ClearDialog();
        }
    }

    private void NextReplica()
    {
        VN_Logger.Log("[VN_DialogModel][NextReplica] Load New Replica: " + m_replicaCount);
        if (m_replicaCount >= m_currentDialog.replicas.Count)
        {
            return;
        }

        var currentReplica = m_currentDialog.replicas[m_replicaCount];
        m_replicaCount++;
        CheckCommand(currentReplica);

        var charactersModel = VN_CharactersModel.Instance();
        if (currentReplica.character != null)
        {
            if (charactersModel)
            {
                charactersModel.SetActiveCharacter(currentReplica.character);
            }
        }
        if (currentReplica.text != null)
        {
            var historyModel = VN_HistoryModel.Instance();
            if (historyModel)
            {
                historyModel.AddText(currentReplica.text);
            }
            if (m_dialogView && charactersModel)
            {
                m_dialogView.NextReplica(charactersModel.GetActiveCharacterColor(), currentReplica.text);
            }
        }
        VN_Logger.Log("[VN_DialogModel][NextReplica] End Load New Replica: " + m_replicaCount + ", replica text: " + currentReplica.text);
    }

    private void CheckCommand(VN_Replica replica)
    {
        if (replica.commands.Count != 0)
        {
            for (int i = 0; i < replica.commands.Count; i++)
            {
                VN_CommandService.RunCommand(replica.commands[i].command, replica.commands[i].commandArg);
            }
        }
    }
}
