using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Confirmer{

    public abstract void createUI();
    public abstract void confirm();
    public abstract void cancel();
    public abstract void reset();
}
