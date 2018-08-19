using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WCCommon {

	public static int EVENT_PLAYER_UNIT_ATTACKED 	= 18;
	public static int EVENT_PLAYER_UNIT_RESCUED 	= 19;

	public static int EVENT_PLAYER_UNIT_DEATH 		= 20;
	public static int EVENT_PLAYER_UNIT_DECAY 		= 21;

	public static int EVENT_PLAYER_UNIT_DETECTED 	= 22;
	public static int EVENT_PLAYER_UNIT_HIDDEN 		= 23;

	//===================================================
	// For use with TriggerRegisterUnitEvent
	//===================================================

	public static int EVENT_UNIT_SPELL_CHANNEL 		= 289;
	public static int EVENT_UNIT_SPELL_CAST 		= 290;
	public static int EVENT_UNIT_SPELL_EFFECT 		= 291;
	public static int EVENT_UNIT_SPELL_FINISH 		= 292;
	public static int EVENT_UNIT_SPELL_ENDCAST 		= 293;

}
