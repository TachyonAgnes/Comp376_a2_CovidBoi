using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject mSwapPrefab;
    [SerializeField] private GameObject mPortalFXPrefab;
    [SerializeField] private GameObject infected_masked;
    [SerializeField] private GameObject infected_nomask;
    [SerializeField] private GameObject infected_masked_isolation;
    [SerializeField] private GameObject infected_nomask_isolation;
    [SerializeField] private GameObject susceptible_nomask;
    [SerializeField] private GameObject susceptible_masked;
    [SerializeField] private GameObject dead_susceptible_nomask;
    [SerializeField] private GameObject dead_susceptible_masked;
    [SerializeField] private GameObject vaccinated_nomask;
    [SerializeField] private GameObject vaccinated_masked;
    [SerializeField] private GameObject unvaccinated_nomask;
    [SerializeField] private GameObject unvaccinated_masked;
    [SerializeField] private GameObject mPortalPrefab;

    GameObject clone = null;
    GameObject dead = null;
    GameObject isolated = null;
    static bool lastBtnState = false;
    public static bool isGoodShot = false;
    public static int perfectCase = 0;
    public static int badCase = 0;
    public static bool isBulletTime = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag != "infected_surface")
        {
            Destroy(col.gameObject);
        }
        if (col.tag == "syringe"){
            isGoodShot = col_syringe();
            if (isBulletTime)
            {
                if (isGoodShot) { perfectCase++; }
                else if (!isGoodShot) { badCase++;}
            }
        }
        else if (col.tag == "mask"){
            isGoodShot = col_mask(gameObject);
            find_a_neighbor_with_no_masks(gameObject);
            if (isBulletTime)
            {
                if (isGoodShot) { perfectCase++; }
                else if (!isGoodShot) { badCase++; }
            }        }
        //note, this one is infector collider on the infected people;
        else if(col.tag == "covid"){
            col_covid();
        }
        else if (col.tag == "infected_surface"){
            col_infected_surface();
        }
        else if (col.tag == "marker"){
            isGoodShot = col_marker();
            if (isBulletTime)
            {
                if (isGoodShot) { perfectCase++; }
                else if(!isGoodShot)  { badCase++; }
            }
        }
        else if (col.tag == "portal_bubble" && !UsingWeapon.isPortalCharged){
            isGoodShot = col_portal_bubble(col);
            if (isBulletTime)
            {
                if (isGoodShot && isBulletTime) { perfectCase++; }
                else if(!isGoodShot)  { badCase++; }
            }
        }
        Destroy(clone, 0.5f);
        Destroy(dead, 0.7f);
        Destroy(isolated, 2.0f);
    }
    private void Update()
    {
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.0f));
        bool leftBtnDown = Input.GetMouseButton(0);
        if (leftBtnDown != lastBtnState)
        {
            // btn down
            if (leftBtnDown && UsingWeapon.isPortalCharged && UsingWeapon.isPortalGun)
            {
                Instantiate(mPortalPrefab, mousePosWorld, transform.rotation);
                string obj = UsingWeapon.objName.Dequeue();

                if (obj.Contains("infected_masked"))
                    Instantiate(infected_masked, mousePosWorld, transform.rotation);
                else if (obj.Contains("infected_nomask"))
                    Instantiate(infected_nomask, mousePosWorld, transform.rotation);
                else if (obj.Contains("susceptible_masked"))
                    Instantiate(susceptible_masked, mousePosWorld, transform.rotation);
                else if (obj.Contains("susceptible_nomask"))
                    Instantiate(susceptible_nomask, mousePosWorld, transform.rotation);
                else if (obj.Contains("vaccinated_masked"))
                    Instantiate(vaccinated_masked, mousePosWorld, transform.rotation);
                else if (obj.Contains("vaccinated_nomask"))
                    Instantiate(vaccinated_nomask, mousePosWorld, transform.rotation);
                else if (obj.Contains("unvaccinated but a mask"))
                    Instantiate(unvaccinated_masked, mousePosWorld, transform.rotation);
                else if (obj.Contains("unvaccinated with nomask"))
                    Instantiate(unvaccinated_nomask, mousePosWorld, transform.rotation);
            }
            lastBtnState = leftBtnDown;
        }
    }

    private void find_a_neighbor_with_no_masks(GameObject gObj)
    {
        print("hey");
        Vector3 center = gObj.transform.position;
        float radius = 1.0f;
        Collider2D[] targets = Physics2D.OverlapCircleAll(center, radius, LayerMask.GetMask("Target"));
        foreach (var tar in targets){
            //add masks;
            if (tar.name != null && gObj != tar)
            {
                if (col_mask(tar.gameObject))
                {
                    ScoreBoard.one_shot_two_mask++;
                    break;
                }
            }
        }
    }
    
    private bool col_syringe()
    {

        if (gameObject.name.Contains("infected_masked"))
        {
            //print("vaccine failed, need isolation");
            return false;
        }
        else if (gameObject.name.Contains("infected_nomask"))
        {
            //print("vaccine failed, need isolation");
            return false;
        }
        else if (gameObject.name.Contains("susceptible_masked"))
        {
            //print("vaccine failed, need more care");
            return false;
        }
        else if (gameObject.name.Contains("susceptible_nomask"))
        {
            //print("vaccine failed, need more care");
            return false;
        }
        else if (gameObject.name.Contains("vaccinated_masked"))
        {
            //print("fine!");
            return false;
        }
        else if (gameObject.name.Contains("vaccinated_nomask"))
        {
            //print("fine!");
            return false;
        }
        else if (gameObject.name.Contains("unvaccinated but a mask"))
        {
            //print("vaccinated, add score");
            clone = Instantiate(mSwapPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Instantiate(vaccinated_masked, transform.position, transform.rotation);
            return true;
        }
        else if (gameObject.name.Contains("unvaccinated with nomask"))
        {
            //print("vaccinated, add score");
            clone = Instantiate(mSwapPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Instantiate(vaccinated_nomask, transform.position, transform.rotation);
            return true;
        }
        return false;
    }
    private bool col_mask(GameObject gObj)
    {
        GameObject cpy = null;
        if (gObj.name.Contains("infected_masked"))
        {
            //print("need isolation");
            return false;
        }
        else if (gObj.name.Contains("infected_nomask"))
        {
            //print("add score");
            cpy = Instantiate(mSwapPrefab, gObj.transform.position, Quaternion.identity);
            Destroy(gObj);
            Instantiate(infected_masked, gObj.transform.position, transform.rotation);
            Destroy(cpy, 0.5f);
            return true;
        }
        else if (gObj.name.Contains("susceptible_masked"))
        {
            //print("need more care");
            return false;
        }
        else if (gObj.name.Contains("susceptible_nomask"))
        {
            //print("add score, need more care");
            cpy = Instantiate(mSwapPrefab, gObj.transform.position, Quaternion.identity);
            Destroy(gObj);
            Instantiate(susceptible_masked, gObj.transform.position, transform.rotation);
            Destroy(cpy, 0.5f);
            return true;
        }
        else if (gObj.name.Contains("vaccinated_masked"))
        {
            //print("fine!");
            return false;
        }
        else if (gObj.name.Contains("vaccinated_nomask"))
        {
            //print("add score");
            cpy = Instantiate(mSwapPrefab, gObj.transform.position, Quaternion.identity);
            Destroy(gObj);
            Instantiate(vaccinated_masked, gObj.transform.position, transform.rotation);
            Destroy(cpy, 0.5f);
            return true;
        }
        else if (gObj.name.Contains("unvaccinated but a mask"))
        {
            //print("need vaccinated");
            return false;
        }
        else if (gObj.name.Contains("unvaccinated with nomask"))
        {
            //print("add score");
            cpy = Instantiate(mSwapPrefab, gObj.transform.position, Quaternion.identity);
            Destroy(gObj);
            Instantiate(unvaccinated_masked, gObj.transform.position, transform.rotation);
            Destroy(cpy, 0.5f);
            return true;
        }
        return false;
    }
    private bool col_covid()
    {
        if (gameObject.name.Contains("unvaccinated with nomask"))
        {
            //print("minus score");
            clone = Instantiate(mSwapPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Instantiate(infected_nomask, transform.position, transform.rotation);
            return true;
        }
        else if (gameObject.name.Contains("susceptible_nomask"))
        {
            //print("die");
            clone = Instantiate(mSwapPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            dead = Instantiate(dead_susceptible_masked, transform.position, transform.rotation);
            return true;
        }
        return false;
    }
    private bool col_infected_surface()
    {
        if (gameObject.name.Contains("unvaccinated but a mask"))
        {
            //print("minus score");
            clone = Instantiate(mSwapPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Instantiate(infected_masked, transform.position, transform.rotation);
            return true;
        }
        else if (gameObject.name.Contains("unvaccinated with nomask"))
        {
            //print("minus score");
            clone = Instantiate(mSwapPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Instantiate(infected_nomask, transform.position, transform.rotation);
            return true;
        }
        else if (gameObject.name.Contains("susceptible_masked"))
        {
            //print("die");
            clone = Instantiate(mSwapPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            dead = Instantiate(dead_susceptible_masked, transform.position, transform.rotation);
            return true;
        }
        else if (gameObject.name.Contains("susceptible_nomask"))
        {
            //print("die");
            clone = Instantiate(mSwapPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            dead = Instantiate(dead_susceptible_nomask, transform.position, transform.rotation);
            return true;
        }
        return false;
    }
    private bool col_marker()
    {
        if (gameObject.name.Contains("infected_masked"))
        {
            //print("minus score");
            clone = Instantiate(mSwapPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            isolated = Instantiate(infected_masked_isolation, transform.position, transform.rotation);
            return true;
        }
        else if (gameObject.name.Contains("infected_nomask"))
        {
            //print("minus score");
            clone = Instantiate(mSwapPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            isolated = Instantiate(infected_nomask_isolation, transform.position, transform.rotation);
            return true;
        }
        return false;
    }
    private bool col_portal_bubble(Collider2D col)
    {
        bool isSuccessful = false;
        //print("what?");
        clone = Instantiate(mPortalFXPrefab, col.transform.position, Quaternion.identity);
        //collectTarget(gameObject.transform.position, 5.0f);
        Vector3 center = gameObject.transform.position;
        float radius = 2.0f;
        Collider2D[] targets = Physics2D.OverlapCircleAll(center, radius, LayerMask.GetMask("Target"));
        if (targets.Length > 1){ ScoreBoard.social_distance++; }
        foreach (var tar in targets)
        {
            if (tar.name != null)
            {
                UsingWeapon.objName.Enqueue(tar.name);
                Destroy(tar.gameObject);
                isSuccessful = true;
            }
        }
        return isSuccessful;
    }

}
