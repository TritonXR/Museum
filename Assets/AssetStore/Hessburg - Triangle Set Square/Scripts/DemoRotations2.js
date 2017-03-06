#pragma strict

var SunLight : Transform;
var CamParent : Transform;
var Triangle1 : Transform;
var Triangle2 : Transform;
var Triangle3 : Transform;

function Start () {

}

function Update () 
{
	SunLight.Rotate(Vector3(20.0* Time.deltaTime, 15.0* Time.deltaTime, 0.0), Space.World);
	Triangle1.Rotate(Vector3(53.0* Time.deltaTime, 50.0* Time.deltaTime, 0.0), Space.World);
	Triangle2.Rotate(Vector3(50.0* Time.deltaTime, 50.0* Time.deltaTime, 0.0), Space.World);
	Triangle3.Rotate(Vector3(47.0* Time.deltaTime, 50.0* Time.deltaTime, 0.0), Space.World);
	CamParent.Rotate(Vector3(0.0, -45.0* Time.deltaTime, 0.0), Space.World);
}