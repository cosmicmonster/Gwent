using System.Collections.Generic;

[System.Serializable]
public class BaseCard {
	
	public int id, type, power = 0;
	public string name = "Unknown Entity";
	
	public BaseCard (int _id, int _type, int _power, string _name) {
		this.id = _id;
		this.type = _type;
		this.power = _power;
		this.name = _name;
	}
}