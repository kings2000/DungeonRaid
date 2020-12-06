using UnityEngine;
using System.Collections;

public class FactoryManager : SinglitonObjects<FactoryManager>
{

    [Header("Ui Factories")]
    [Tooltip("NOTE!: Assignment of any Factories in the Editor should be treated temporary")]
    public UiItemFactory uiItemFactory;

}
