using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    public Transform AteamPrefab;
    public Transform BteamPrefab;

    void Start()
    {
        GenerateTeam();
    }
    public void GenerateTeam()
    {
        string AteamName = "A team";
        string BteamName = "B team";

        if (transform.Find(AteamName) || transform.Find(BteamName))
        {
            DestroyImmediate(transform.Find(AteamName).gameObject); //이미 있으면 삭제하고 생성
            DestroyImmediate(transform.Find(BteamName).gameObject);
        }

        Transform Ateamholder = new GameObject(AteamName).transform;
        Transform Bteamholder = new GameObject(BteamName).transform;
        Ateamholder.parent = transform;
        Bteamholder.parent = transform;

        //team Character 생성
        int CharacterNum = 7;
        int offset = 3;
        for(int i=1; i < CharacterNum+1; i++)
        {
            string ACharacterName = "A" + i.ToString();
            string BCharacterName = "B" + i.ToString();
            Vector3 ACharacterPosition = new Vector3(-20 + offset*i,0,-15);//서버로부터 받은 position 저장
            Vector3 BCharacterPosition = new Vector3(-20 + offset * i,0 , 15);
            Transform newACharacter = Instantiate(AteamPrefab, ACharacterPosition, Quaternion.Euler(new Vector3(0,0,0))) as Transform;
            Transform newBCharacter = Instantiate(BteamPrefab, BCharacterPosition, Quaternion.Euler(new Vector3(0,180, 0))) as Transform;
            newACharacter.name = ACharacterName;
            newBCharacter.name = BCharacterName;

            newACharacter.parent = Ateamholder;
            newBCharacter.parent = Bteamholder;
        }
        

    }
}
