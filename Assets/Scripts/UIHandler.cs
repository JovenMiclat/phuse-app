using Assets.Scripts.AlgorithmHandler;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddToCart
{
    public int totalOfItems { get; set; }
}
public class UIHandler : MonoBehaviour
{
    [Header("MAIN MENU")]
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject ModelCanvas;
    [SerializeField] Button showCart;
    [SerializeField] TMP_Text addToCartInt;
    [SerializeField] GameObject modalPopup;
    [SerializeField] Button doneButton;
    public Button[] itemsButtons;
    [Header("ADD TO CART MENU")]
    [Space(15)]
    [SerializeField] GameObject AddToCartPanel;
    [SerializeField] TMP_Text totalOfItems;
    [SerializeField] Button closeCart;
    [SerializeField] Button ShowRoute;
    [SerializeField] Button[] removeItems;
    public List<String> itemNames = new List<String>();
    public GameObject[] itemCart;
    [Header("MODAL PROPERTIES")]
    [SerializeField] Image itemLogo;
    [SerializeField] TMP_Text ModalTitle;
    [SerializeField] TMP_Text itemDescription;
    [SerializeField] Button closeModal;
    [SerializeField] Button AddToCart;
    [SerializeField] public TMP_Text priceDisplay;
    AddToCart cart = new AddToCart();
    public List<int> item_arr2 = new List<int>();

	int numb;
    APathfinder pathfinder;
    bool isSelected = false;

    [Obsolete]
    void Start()
    {
        pathfinder = GetComponent<APathfinder>();
		
        showCart.onClick.AddListener(OpenCart);
        closeCart.onClick.AddListener(CloseCart);
        closeModal.onClick.AddListener(onCloseModal);
        ShowRoute.onClick.AddListener(onAddRoute);
        doneButton.onClick.AddListener(onDoneClick);

        setAllButtons();
    }

    [Obsolete]
    void onDoneClick()
    {
        SceneManager.LoadScene(1);
    }
    void OpenCart()
    {
        this.MainMenuPanel.SetActive(false);
        this.AddToCartPanel.SetActive(true);
    }
    void CloseCart()
    {
        this.MainMenuPanel.SetActive(true);
        this.AddToCartPanel.SetActive(false);
    }
    void onAddRoute()
    {
        if (pathfinder.enabled == false)
        {
            pathfinder.enabled = true;
        }
        else
        {
            pathfinder.enabled = false;
        }
        this.MainMenuPanel.SetActive(false);
        this.AddToCartPanel.SetActive(false);
        ModelCanvas.SetActive(true);
    }

    [Obsolete]
    void setAllButtons()
    {

        for (int i = 0; i < itemsButtons.Length; i++)
        {
            int closureIndex = i;
            string[] names;
            var Title = itemsButtons[i].GetComponentInChildren<TMP_Text>();
            Title.text = itemsButtons[i].name;
            itemNames.Add(Title.text);
            names = itemNames.ToArray();

            itemsButtons[closureIndex].onClick.AddListener(() => TaskOnClick(closureIndex, Title.text));
        }
        for (int j = 0; j < itemCart.Length; j++)
        {
            itemCart[j].name = itemsButtons[j].name;
            var Title = itemCart[j].GetComponentInChildren<TMP_Text>();
		
			Title.text = itemCart[j].name;
        }
        for (int k = 0; k < removeItems.Length; k++)
        {
            int closureIndex = k;
			removeItems[k] = itemCart[k].gameObject.GetComponentInChildren<Button>();
            removeItems[k].onClick.AddListener(() => onRemoveItems(closureIndex));
        }
    }

    [Obsolete]
    public void TaskOnClick(int buttonIndex, string title)
    {
        if (isSelected == false)
        {
            ModalTitle.text = title;
            isSelected = true;
            modalPopup.SetActive(true);
            AddToCart.onClick.AddListener(() => onAddToCart(buttonIndex));

            switch (buttonIndex)
            {
                case 0:
					itemDescription.text = "Description: Quality Loaf Bread";
                    priceDisplay.text = "Price: 70 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://ph-live-01.slatic.net/p/f1e3a1cd326e097d42f8db0794505bfd.jpg", itemLogo));
                    break;
                case 1:
                    itemDescription.text = "Description: Box of Fresh Milk ";
                    priceDisplay.text = "Price: 90 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://store.iloilosupermart.com/wp-content/uploads/2020/05/265.jpg", itemLogo));
                    break;
                case 2:
                    itemDescription.text = "Description: Pack of Hygienic Wiping Material";
                    priceDisplay.text = "Price: 60 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("http://cdn.shopify.com/s/files/1/0421/2454/2105/products/4806502359440_f654d714-f61b-4808-aeed-55be66b97db1.jpg?v=1628438290", itemLogo));
                    break;
                case 3:
                    itemDescription.text = "Description: Common Breakfast Meal";
                    priceDisplay.text = "Price: 220 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cdn.shopify.com/s/files/1/0260/6877/9066/products/110931_cf464179-f8b9-4f7b-8260-5ed9ccbf092d_416x700.jpg?v=1625664324", itemLogo));
                    break;
                case 4:
                    itemDescription.text = "Description: Breakfast Meal from Chicken farm";
                    priceDisplay.text = "Price: 10 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://solidstarts.com/wp-content/uploads/when-can-babies-eat-eggs.jpg", itemLogo));
                    break;
                case 5:
                    itemDescription.text = "Description: One of the Most Common Ingredients in a Filipino Dish";
                    priceDisplay.text = "Price: 63 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://solidstarts.com/wp-content/uploads/garlic_edited-scaled.jpg", itemLogo));
                    break;
                case 6:
                    itemDescription.text = "Description: a Significant Ingredient in a Filipino Dish";
                    priceDisplay.text = "Price: 149 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://solidstarts.com/wp-content/uploads/Onions-for-Babies-scaled.jpg", itemLogo));
                    break;
                case 7:
                    itemDescription.text = "Description: Bottle of Brandy";
                    priceDisplay.text = "Price: 280 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://manila-wine.com/media/catalog/product/a/l/alfonso-i-light-1l.jpg", itemLogo));
                    break;
                case 8:
                    itemDescription.text = "Description: Drumstick of Ice Cream";
                    priceDisplay.text = "Price: 30 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://www.selectaphilippines.com/content/dam/unilever/cornetto/australia/pack_shot/8999999056193_cornettovanilla-1249093-png.png", itemLogo));
                    break;
                case 9:
                    itemDescription.text = "Description: Perfect Breakfast Alternative";
                    priceDisplay.text = "Price: 120 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://csistore.ph/wp-content/uploads/2020/07/Quaker-Oats-Instant-800g.jpg", itemLogo));
                    break;
                case 10:
                    itemDescription.text = "Description: Hygienic Wiping Material";
                    priceDisplay.text = "Price: 148 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/7ce11c49472175f317fda78b14f4d9d5", itemLogo));
                    break;
                case 11:
                    itemDescription.text = "Description: Fragrant Sanitary Spray";
                    priceDisplay.text = "Price: 235 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/49178169f29c1e0c8de142312d9d5665", itemLogo));
                    break;
                case 12:
                    itemDescription.text = "Description: Laundry Soap";
                    priceDisplay.text = "Price: 122 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/773998b9e0acf168e654613ce8a3f02d", itemLogo));
                    break;
                case 13:
                    itemDescription.text = "Description: Dishwashing Liquid";
                    priceDisplay.text = "Price: 70 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/fc8c012bf263129ae6dc3f75d7108984", itemLogo));
                    break;
                case 14:
                    itemDescription.text = "Description: Maximum Flouride Toothpaste";
                    priceDisplay.text = "Price: 40 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/97f2189e614dc4471bd36b116df48996", itemLogo));
                    break;
                case 15:
                    itemDescription.text = "Description: Bleach for Colored Fabrics";
                    priceDisplay.text = "Price: 55 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/350d1f8953ceb5bc8e97e1b9f57fe6b0", itemLogo));
                    break;
                case 16:
                    itemDescription.text = "Description: Body Soap used to Protect Against Most Germs";
                    priceDisplay.text = "Price: 20 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
                    StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/fd772c8f9219932ab12dc31a7fb29b37", itemLogo));
                    break;
                case 17:
                    itemDescription.text = "Description: Hair Shampoo ";
                    priceDisplay.text = "Price: 90 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
                    StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/bca19b350086f0ea4d786d6c379d2674", itemLogo));
                    break;
                case 18:
                    itemDescription.text = "Description: Rubbing Alcohol";
                    priceDisplay.text = "Price: 55 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
                    StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/37e87a9386301a7c10fcf01a7e6e9b2e", itemLogo));
                    break;
                case 19:
                    itemDescription.text = "Description: Cotton Used for Sanitation";
                    priceDisplay.text = "Price: 50 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
                    StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/90207d7626834c11612cfa33661ccb83", itemLogo));
                    break;
				case 20:
					itemDescription.text = "Description: Sandwich Spread / Dish Ingredient";
					priceDisplay.text = "Price: 96 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/2bf7a243136a4b0d201882c8d0bc290a", itemLogo));
					break;
				case 21:
					itemDescription.text = "Description: Sandwich Essential/ Dish Ingredient";
					priceDisplay.text = "Price: 105 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/6930c0e0af739b0ba8519a6788c15244", itemLogo));
					break;
				case 22:
					itemDescription.text = "Description: Cooking Oil";
					priceDisplay.text = "Price: 250 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/85069d1cdd66dab1fac59d6371596206", itemLogo));
					break;
				case 23:
					itemDescription.text = "Description: Cooking Condiment";
					priceDisplay.text = "Price: 47 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/196e46602c6c1ba74be26e4095326fd0", itemLogo));
					break;
				case 24:
					itemDescription.text = "Description: Cooking Ingredient";
					priceDisplay.text = "Price: 47 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/37628f6880c7dc3263a6d6d528b9555a", itemLogo));
					break;
				case 25:
					itemDescription.text = "Description: Common Breakfast Dip Sauce";
					priceDisplay.text = "Price: 36 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/36d29b7db40c8107dc35be750565beeb", itemLogo));
					break;
				case 26:
					itemDescription.text = "Description: Can of Corned Beef";
					priceDisplay.text = "Price: 60 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/d7713c9026f0d5f3cd0145039843137c", itemLogo));
					break;
				case 27:
					itemDescription.text = "Description: Can of Sardines";
					priceDisplay.text = "Price: 19 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/5d2e6fa4bd7343abcbbbcf4535992bc2", itemLogo));
					break;
				case 28:
					itemDescription.text = "Description: Can of Tuna";
					priceDisplay.text = "Price: 20 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/5d3e0b8625199fb8d2e55287181a4ae6", itemLogo));
					break;
				case 29:
					itemDescription.text = "Description: Instant Coffee in Sachet";
					priceDisplay.text = "Price: 147 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/95fadda1e54955657b705bde2a6b2a15", itemLogo));
					break;
				case 30:
					itemDescription.text = "Description: Heavy Duty Scrub Sponge";
					priceDisplay.text = "Price: 95 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cdn.shopify.com/s/files/1/0505/7144/4423/products/111411_1aa35b50-b34c-48e2-b2ab-4bfca6879aed.jpg?v=1624029633", itemLogo));
					break;
				case 31:
					itemDescription.text = "Description: Fried Chicken Coating";
					priceDisplay.text = "Price: 20 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cdn.shopify.com/s/files/1/0338/0694/2253/products/Ajinomoto-Crispy-Fry-Original-60g_FRONT_2048x.jpg?v=1624639309", itemLogo));
					break;
				case 32:
					itemDescription.text = "Description: Sachet of MSG";
					priceDisplay.text = "Price: 55 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("http://cdn.shopify.com/s/files/1/0420/7404/6615/products/ajinomoto.jpg?v=1617756782", itemLogo));
					break;
				case 33:
					itemDescription.text = "Description: Cube of Pork Broth Flavor";
					priceDisplay.text = "Price: 64 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://www.knorr.com/content/dam/unilever/knorr_world/global/packaging_artwork/knorr_pork_cubes_60_g-55743020-png.png", itemLogo));
					break;
				case 34:
					itemDescription.text = "Description: 2L Bottle of Soda";
					priceDisplay.text = "Price: 69 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://supplies-eu.com/wp-content/uploads/2021/05/Coca-Cola-2L.jpeg", itemLogo));
					break;
				case 35:
					itemDescription.text = "Description: 500 ml bottle of Energy Drink";
					priceDisplay.text = "Price: 37 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/41ec040e9febe834f004204272bad19a", itemLogo));
					break;
				case 36:
					itemDescription.text = "Description: Common Breakfast Meal";
					priceDisplay.text = "Price: 68 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://images.freshop.com/1564405684703975532/90789886ef1105c8fe4684c8c5624318_large.png", itemLogo));
					break;
				case 37:
					itemDescription.text = "Description: Pack of Easy Cook Snack";
					priceDisplay.text = "Price: 75 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://ipcdn.freshop.com/resize?url=https://images.freshop.com/1564405684702551728/d4058a6ae8295a4e04c2538c80ba95be_large.png&width=512&type=webp&quality=90", itemLogo));
					break;
				case 38:
					itemDescription.text = "Description: 250g Slice of Sweet Ham";
					priceDisplay.text = "Price: 65 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://www.shopmagic.ph/images/detailed/13/PAMPANGA'S_BEST_SWEET_HAM_250G.jpg", itemLogo));
					break;
				case 39:
					itemDescription.text = "Description: 200g Chicken Nuggets";
					priceDisplay.text = "Price: 89 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cdn.shopify.com/s/files/1/0060/6067/1058/products/chickennuggets200_470x.jpg?v=1595662178", itemLogo));
					break;
				case 40:
					itemDescription.text = "Description: Pack of Easy Cook Snack";
					priceDisplay.text = "Price: 156 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://media.karousell.com/media/photos/products/2021/5/7/shanghai_kikiam_500grams_1620427740_5fe214d3.jpg", itemLogo));
					break;
				case 41:
					itemDescription.text = "Description: Packed Bottle of 2L Soda";
					priceDisplay.text = "Price: 360 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://www.pngitem.com/pimgs/m/519-5194293_pack-coca-cola-1-5-hd-png-download.png", itemLogo));
					break;
				case 42:
					itemDescription.text = "Description: Round Bottle of Gin";
					priceDisplay.text = "Price: 65 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://imartgrocersph.com/wp-content/uploads/2020/09/gin-bilog-350ml.png", itemLogo));
					break;
				case 43:
					itemDescription.text = "Description: Box of Fresh Milk";
					priceDisplay.text = "Price: 95 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cdn.shopify.com/s/files/1/0226/7368/6602/products/COWHEADFRESHMILK1L_900x900.jpg?v=1617102741", itemLogo));
					break;
				case 44:
					itemDescription.text = "Description: Can of Assorted Biscuits";
					priceDisplay.text = "Price: 413 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://ph-test-11.slatic.net/p/mdc/5f7b8f695e3d782db6e65b5c724e7e00.jpg", itemLogo));
					break;
				case 45:
					itemDescription.text = "Description: Pack of Candle Lights";
					priceDisplay.text = "Price: 50 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://store.iloilosupermart.com/wp-content/uploads/2020/05/IMG_20200522_145102-Large.jpg", itemLogo));
					break;
				case 46:
					itemDescription.text = "Description: One Pack of Spaghetti Ingredients";
					priceDisplay.text = "Price: 125 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("http://cdn.shopify.com/s/files/1/0260/6877/9066/products/451555_a36c5cad-6e23-429d-8210-109fb938ea8b.jpg?v=1638153453", itemLogo));
					break;
				case 47:
					itemDescription.text = "Description: Quality Loaf Bread";
					priceDisplay.text = "Price: 65 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://ph-test-11.slatic.net/p/1a1e722e468a4e4104d5b3f7cac86085.jpg", itemLogo));
					break;
				case 48:
					itemDescription.text = "Description: Canned Fruit that can be used for Cooking";
					priceDisplay.text = "Price: 75 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://ph-test-11.slatic.net/p/a282cad67f00e05757f26e843a992bdc.jpg", itemLogo));
					break;
				case 49:
					itemDescription.text = "Description: Pack of Hygienic Wiping Material";
					priceDisplay.text = "Price: 63 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://ph-test-11.slatic.net/p/mdc/3ed7187a48f2241f114000ebcadae9ad.jpg", itemLogo));
					break;
				case 50:
					itemDescription.text = "Description: Diaper for Infant's Comfort";
					priceDisplay.text = "Price: 282 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/adde8b3b52251fe08f3135b6ced172b1", itemLogo));
					break;
				case 51:
					itemDescription.text = "Description: Pack of Hygienic Wiping Material";
					priceDisplay.text = "Price: 13 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/077463238e13f01c68a77ab2ddcc5b48", itemLogo));
					break;
				case 52:
					itemDescription.text = "Description: Hygienic Feminine Material";
					priceDisplay.text = "Price: 21 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/bf4f1bebbad0a0f817e4799fd14d4125", itemLogo));
					break;
				case 53:
					itemDescription.text = "Description: Disposable Plate";
					priceDisplay.text = "Price: 75 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/ea44fa1ab5e60ac67dfd55ed15f77cd7", itemLogo));
					break;
				case 54:
					itemDescription.text = "Description: Microwaveable Container";
					priceDisplay.text = "Price: 40 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/0c5f43280c248859d77a5a7b9bb8ae4b", itemLogo));
					break;
				case 55:
					itemDescription.text = "Description: Disposable Plastic Cups";
					priceDisplay.text = "Price: 37 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/cc954486be76d6fc414834f770654ef8", itemLogo));
					break;
				case 56:
					itemDescription.text = "Description: Fabric Heat Reducer";
					priceDisplay.text = "Price: 9 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/be89cdd0e10266af21e2b885c5857daa", itemLogo));
					break;
				case 57:
					itemDescription.text = "Description: Mosquito Repellant Spray";
					priceDisplay.text = "Price: 263 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/1d99fb85eab8d8adec4890353c22975c", itemLogo));
					break;
				case 58:
					itemDescription.text = "Description: Mouse Repellent Trap";
					priceDisplay.text = "Price: 11 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/e563debcdc9f652219c665eacc44656a", itemLogo));
					break;
				case 59:
					itemDescription.text = "Description: Bathroom Cleaner";
					priceDisplay.text = "Price: 148 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/5c66e67010d23823517bd8bbe0a5350e", itemLogo));
					break;
				case 60:
					itemDescription.text = "Description: Dishwashing Paste";
					priceDisplay.text = "Price: 46 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/6ae72e69bca65f2cb257527355667131", itemLogo));
					break;
				case 61:
					itemDescription.text = "Description: Canned Fruits";
					priceDisplay.text = "Price: 132 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/dd0ac6447a3952e055c854c8d9d764c9", itemLogo));
					break;
				case 62:
					itemDescription.text = "Description: Macaroni Shaped Pasta";
					priceDisplay.text = "Price: 80 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/f322371ed6ee99f25ed972d8b157d0b1", itemLogo));
					break;
				case 63:
					itemDescription.text = "Description: Spaghetti Ingredient";
					priceDisplay.text = "Price: 88 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/7dfddda4299caf035638a6433c451665", itemLogo));
					break;
				case 64:
					itemDescription.text = "Description: Instant Coffee in Sachet";
					priceDisplay.text = "Price: 46 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/26054b7b4ddf98d031d42618ddfd5494", itemLogo));
					break;
				case 65:
					itemDescription.text = "Description: Pancake Mix with Syrup";
					priceDisplay.text = "Price: 71 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/516c109177b7315376ebfd69817256ca", itemLogo));
					break;
				case 66:
					itemDescription.text = "Description: Baking Powder";
					priceDisplay.text = "Price: 50 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/35e5a56fe8b2e82ceab6965acb4452de", itemLogo));
					break;
				case 67:
					itemDescription.text = "Description: Can of Pineapple Juice";
					priceDisplay.text = "Price: 81 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/072e269aaa8f76383efb531a3db336f2", itemLogo));
					break;
				case 68:
					itemDescription.text = "Description: 90ml Box of Strawberry Drink";
					priceDisplay.text = "Price: 43 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/201f3d78c8ffd6732eaa191c0763813a", itemLogo));
					break;
				case 69:
					itemDescription.text = "Description: Hygienic Feminine Material";
					priceDisplay.text = "Price: 77 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/7c6d409f69b9878e947c1c2de24b5478", itemLogo));
					break;
				case 70:
					itemDescription.text = "Description: 65g Laundry Soap";
					priceDisplay.text = "Price: 45 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/c78c492267987772d43a01b8cd0886dc", itemLogo));
					break;
				case 71:
					itemDescription.text = "Description: 1kg Laundry Soap";
					priceDisplay.text = "Price: 115 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://didwblovvwmoz.cloudfront.net/product_images/3306569/1255746/live/featured_image/262dd9690c2a35409185aea7b783_864_882.jpeg", itemLogo));
					break;
				case 72:
					itemDescription.text = "Description: Sachet of Fabric Conditioner";
					priceDisplay.text = "Price: 50 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/b51fd62134708c10cf3a1fc309a16758", itemLogo));
					break;
				case 73:
					itemDescription.text = "Description: Pack of Hygienic Wiping Material";
					priceDisplay.text = "Price: 135 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://www.shophygiene.com.ph/pub/media/catalog/product/cache/6e9e63b8ef9632875a86ea2b8ecfe401/f/e/femme_interfolded_paper_towel_pack_of_3_-1.jpg", itemLogo));
					break;
				case 74:
					itemDescription.text = "Description: 850ml Bottle of Soda";
					priceDisplay.text = "Price: 59 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/7e14d90003e8cc010ffa285605c13676", itemLogo));
					break;
				case 75:
					itemDescription.text = "Description: 1.5L Bottle of Energy Drink";
					priceDisplay.text = "Price: 120 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://ph-test-11.slatic.net/p/2adb7532f29b0df7c3d0084abdeafeb2.jpg", itemLogo));
					break;
				case 76:
					itemDescription.text = "Description: 500ml of Mineral Water";
					priceDisplay.text = "Price: 15 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/c14bbbdcc7e06493f35ba98265bb9c3b", itemLogo));
					break;
				case 77:
					itemDescription.text = "Description: 330ml Bottle of Light Alcoholic Beverage";
					priceDisplay.text = "Price: 50 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://ph-test-11.slatic.net/p/d0ba79b8fe549869e3e0b19f73b5b4be.png", itemLogo));
					break;
				case 78:
					itemDescription.text = "Description: 1L Bottle of Alcoholic Beverage";
					priceDisplay.text = "Price: 90 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://www.shopnsell.com.ph/sns-assets/uploads/2020/09/RED-HORSE-1000ML.png", itemLogo));
					break;
				case 79:
					itemDescription.text = "Description: 500ml Bottle of Apple Flavored Green Tea";
					priceDisplay.text = "Price: 26 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://ph-test-11.slatic.net/p/37af8064b4f38382ea535337ed9c4cc5.jpg", itemLogo));
					break;
				case 80:
					itemDescription.text = "Description: 1pc of Orange Fruit";
					priceDisplay.text = "Price: 30 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://thegreengrocermanila.com/wp-content/uploads/2015/12/orange-sunkist-seedless-1.jpg", itemLogo));
					break;
				case 81:
					itemDescription.text = "Description: 1pc of Apple Fruit";
					priceDisplay.text = "Price: 70 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://www.collinsdictionary.com/images/full/apple_158989157.jpg", itemLogo));
					break;
				case 82:
					itemDescription.text = "Description: Cooking Butter / Food Ingredient";
					priceDisplay.text = "Price: 49 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://mygroceryph.com/pub/media/catalog/product/cache/942fb7ebd8f124d7d8ad39312cbc983a/d/a/daricreme-225g-.png", itemLogo));
					break;
				case 83:
					itemDescription.text = "Description: 750ml Pint of Ice Cream";
					priceDisplay.text = "Price: 115 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://www.selectaphilippines.com/content/dam/unilever/heart/philippines/pack_shot/4800086038647-1383335-png.png", itemLogo));
					break;
				case 84:
					itemDescription.text = "Description: 1.5L Pint of Ice Cream";
					priceDisplay.text = "Price: 230 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://ph-live-01.slatic.net/p/72136969288a3cb235136c9413b531ae.jpg", itemLogo));
					break;
				case 85:
					itemDescription.text = "Description: Home Appliance for Boiling Water";
					priceDisplay.text = "Price: 169 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://cf.shopee.ph/file/8413dc63a75890df5b95d9f81f7a6553", itemLogo));
					break;
				case 86:
					itemDescription.text = "Description: Home Appliance for Blending Food Ingredients";
					priceDisplay.text = "Price: 1,109 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://www.abenson.com/media/catalog/product/cache/1801c418208f9607a371e61f8d9184d9/9/8/98421_2020.jpg", itemLogo));
					break;
				case 87:
					itemDescription.text = "Description: Home Appliance for Straightening Fabrics";
					priceDisplay.text = "Price: 519 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://www.abenson.com/media/catalog/product/cache/1801c418208f9607a371e61f8d9184d9/1/4/145582_2020_1.jpg", itemLogo));
					break;
				case 88:
					itemDescription.text = "Description: Home Appliance for Unclogging toilet";
					priceDisplay.text = "Price: 51 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://www.thespruce.com/thmb/4IkYyK9CGJHV5_yNnORds9k0fSc=/2121x1414/filters:fill(auto,1)/GettyImages-173683465-58f822b83df78ca159d4543a.jpg", itemLogo));
					break;
				case 89:
					itemDescription.text = "Description: Home Appliance for Wiping Floor Dusts";
					priceDisplay.text = "Price: 100 Pesos";
					itemCart[buttonIndex].gameObject.GetComponentInChildren<Text>().text = priceDisplay.text;
					StartCoroutine(loadImageFromURL("https://ml5ispwgxjkd.i.optimole.com/kskrOOQ-Err4cJvx/w:499/h:499/q:auto/https://gcquick.com/wp-content/uploads/2020/07/baguio_walis_tambo1.jpg", itemLogo));
					break;

                default:
                    Debug.Log("No Description Found");
                    break;
            }
        }
    }

    [Obsolete]
    IEnumerator loadImageFromURL(string url, Image img)
    {
        WWW www = new WWW(url);
        yield return www;
        img.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
    void onRemoveItems(int buttonIndex)
    {
        itemCart[buttonIndex].SetActive(false);
		item_arr2.Remove(buttonIndex);
		numb--;
	}
    void onAddToCart(int buttonIndex)
    {
		Debug.Log(buttonIndex);
        itemCart[buttonIndex].SetActive(true);
        item_arr2.Add(buttonIndex);
		numb++;
    }

    void onCloseModal()
    {
        modalPopup.SetActive(false);
        isSelected = false;
        AddToCart.onClick.RemoveAllListeners();
    }
    private void FixedUpdate()
    {
		cart.totalOfItems = numb;
		addToCartInt.text = numb.ToString();
		totalOfItems.text = "Total of Items: " + numb;
	}
}
