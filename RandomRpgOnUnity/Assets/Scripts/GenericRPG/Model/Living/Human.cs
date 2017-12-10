using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GenericRpg.Business.Model.Living
{
    public class Human : Being
    {
        float resetTimer = 0;

        int itertionNumber = 0;
        private Sprite redhumanSprite;
        private Sprite whiteHumanSprite;

        Vector2 direction;

        private HumanTarget humanTarget;
        // Use this for initialization
        new void Start()
        {
            base.Start();
            direction = GetNewDirection();
            redhumanSprite = Resources.Load("Human2", typeof(Sprite)) as Sprite;
            whiteHumanSprite = Resources.Load("Human", typeof(Sprite)) as Sprite;
            humanTarget = GetHumanTarget();
        }

        private class HumanTarget
        {
            public Human Human { get; set; }
            public float Distance { get; set; }
        }

        HumanTarget GetHumanTarget()
        {

            float minDistanceSoFar = float.MaxValue;
            HumanTarget chosenTarget = null;
            foreach (var obj in GameObject.FindObjectsOfType(this.GetType()))
            {

                Human currentlyChecked = obj as Human;
                if (currentlyChecked != null && currentlyChecked != this && currentlyChecked.IsAlive)
                {
                    if (Vector2.Distance(transform.position, currentlyChecked.transform.position) < minDistanceSoFar)
                    {

                        minDistanceSoFar = Vector2.Distance(transform.position, currentlyChecked.transform.position);
                        chosenTarget = new HumanTarget
                        {
                            Distance = minDistanceSoFar,
                            Human = currentlyChecked
                        };
                    }
                }
            }
            return chosenTarget;
        }

        public void ToRedHuman() {
            SpriteRenderer rendrer = gameObject.GetComponent<SpriteRenderer>();
            rendrer.sprite = redhumanSprite;
        }

        public void ToWhiteHuman() {
            SpriteRenderer rendrer = gameObject.GetComponent<SpriteRenderer>();
            rendrer.sprite = whiteHumanSprite;
        }
        float whenToGoWhile = 0;
        public bool needtoGoWhile = false;
        private float whenToCheckForNewTarget = 0;
        // Update is called once per frame
        void Update()
        {
            if (needtoGoWhile && whenToGoWhile < Time.time)
            {
                needtoGoWhile = false;
                ToWhiteHuman();
            }
            if (whenToCheckForNewTarget < Time.time)
            {
                whenToCheckForNewTarget = Time.time + 1;
                humanTarget = GetHumanTarget();
            }
            

            if (!IsAlive)
            {

                ToRedHuman();
                GameObject.Destroy(gameObject.GetComponent<Rigidbody2D>());
                GameObject.Destroy(gameObject.GetComponent<BoxCollider2D>());

                this.Decompose();
                if (this.IsDecomposed)
                {
                    GameObject.Destroy(gameObject);
                    return;
                }
                return;
            }
            itertionNumber++;


            HumanTarget target = GetHumanTarget();

            if (target == null)
            {
                if (itertionNumber > 100)
                {
                    direction = GetNewDirection();
                    itertionNumber = 0;
                }

                transform.Translate(direction * Time.deltaTime);
            }
            else
            {

                if (target.Distance < (this.CurrentWeapon.Range * 0.2f))
                {
                    if (Time.time > resetTimer)
                    {
                        base.Attack(target.Human);
                     
                        resetTimer = Time.time + this.CurrentWeapon.RewindPeriod; 
                    }
                }else
                {
                    Vector2 targetPosition = target.Human.transform.position;
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, Attributes.Speed.Value);
                    
                }

            }

        }

        public override void ShowHit()
        {
            this.ToRedHuman();
            whenToGoWhile = Time.time + 0.15f;
            needtoGoWhile = true;
        }



        private void OnCollisionEnter2D(Collision2D collision)
        {
          
        }


        private Vector3 GetNewDirection()
        {
            int d = UnityEngine.Random.Range(1, 4);
            switch (d)
            {
                case 1: return Vector2.up; break;
                case 2: return Vector2.down; break;
                case 3: return Vector2.left; break;
                case 4: return Vector2.right; break;
                default: throw new System.Exception("Impossible");
            }
        }
    }
}
