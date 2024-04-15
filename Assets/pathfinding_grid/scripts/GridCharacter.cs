using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GridCharacter : MonoBehaviour
{
    public grid_manager gm_s;
    public bool big;
    public bool body_looking;
    public bool moving;
    public bool moving_tiles;
    public float move_speed = 2f;
    public float rotate_speed = 6f;
    public Color col;
    public Transform tr_body;
    public tile tile_s;
    public tile tar_tile_s;
    public tile selected_tile_s;
    public List<Transform> db_moves;
    public int max_tiles = 7;
    public int num_tile;

    public event Action PathfindingCompleted;
    private Vector3 LookVectorWhenComplete = Vector3.forward;
    void Awake() {
        SceneManager.sceneLoaded += ReassignGrid;
    }

    private void ReassignGrid(Scene arg0, LoadSceneMode arg1)
    {
        gm_s = FindObjectOfType<grid_manager>();
        Debug.Log("Reassiging grid...");
        Debug.Log(gm_s == null);
    }
    void Update()
    {
        if (body_looking)
        {
            Vector3 tar_dir = db_moves[1].position - tr_body.position;
            tar_dir.y = 0; // Ensure no rotation in the y-axis
            Quaternion new_rot = Quaternion.LookRotation(tar_dir);
            tr_body.transform.rotation = new_rot;
        }

        if (moving)
        {
            float step = move_speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, db_moves[0].position, step);
            var tdist = Vector3.Distance(tr_body.position, db_moves[0].position);
            if (tdist < 0.001f)
            {
                tile_s.db_chars.Remove(this);
                tile_s = tar_tile_s.db_path_lowest[num_tile];
                tile_s.db_chars.Add(this);
                if (moving_tiles && num_tile < tar_tile_s.db_path_lowest.Count - 1)
                {
                    num_tile++;
                    var tpos = tar_tile_s.db_path_lowest[num_tile].transform.position;
                    if (big) //Large chars//
                    {
                        tpos = new Vector3(0, 0, 0);
                        tpos += tar_tile_s.db_path_lowest[num_tile].transform.position + tar_tile_s.db_path_lowest[num_tile].db_neighbors[1].tile_s.transform.position + tar_tile_s.db_path_lowest[num_tile].db_neighbors[2].tile_s.transform.position + tar_tile_s.db_path_lowest[num_tile].db_neighbors[1].tile_s.db_neighbors[2].tile_s.transform.position;
                        tpos /= 4; //Takes up 4 tiles//
                    }
                    tpos.y = transform.position.y;
                    db_moves[0].position = tpos;
                    db_moves[1].position = tpos;
                }
                else
                {
                    PathfindingCompleted?.Invoke();

                    body_looking = false;

                    SetLookRot(LookVectorWhenComplete);

                    db_moves[4].gameObject.SetActive(false);
                    moving = false;
                    moving_tiles = false;
                    if (gm_s.find_path == efind_path.once_per_turn || gm_s.find_path == efind_path.max_tiles)
                        gm_s.find_paths_static(this);
                    gm_s.hover_tile(selected_tile_s);
                }
            }
        }
    }
    public void SetLookRotWhenComplete(Vector3 lookRot) {
        LookVectorWhenComplete = lookRot;
    }
    private void SetLookRot(Vector3 lookRot) {
        Debug.Log("SET LOOK ROT to VECTOR" + lookRot);
        Quaternion new_rot = Quaternion.LookRotation(lookRot);
        tr_body.transform.rotation = new_rot;
        LookVectorWhenComplete = Vector3.forward;
    }
    public void SetArbitraryRot(Vector3 rot) {
        Quaternion new_rot = Quaternion.LookRotation(rot);
        tr_body.transform.rotation = new_rot;
    } 

    public void move_tile(tile ttile)
    {
        if (moving) // Cancel the current movement if character is already moving
        {
            moving = false;
            moving_tiles = false;
            db_moves[4].gameObject.SetActive(false);
            tile_s.db_chars.Remove(this);
            if (gm_s.find_path == efind_path.once_per_turn || gm_s.find_path == efind_path.max_tiles)
                gm_s.find_paths_static(this);
        }

        num_tile = 0;
        tar_tile_s = ttile;

        //0 - body_move, 1 - body_look, 2 - head_look, 3 - eyes_look, target tile marker
        db_moves[0].parent = null;
        db_moves[1].parent = null;
        db_moves[4].parent = null;

        move_speed = 2;

        var tpos = new Vector3(0, 0, 0);
        if (!big)
        {
            tpos = tar_tile_s.transform.position;
        }
        else if (big)
        {
            tpos += tar_tile_s.transform.position + tar_tile_s.db_neighbors[1].tile_s.transform.position + tar_tile_s.db_neighbors[2].tile_s.transform.position + tar_tile_s.db_neighbors[1].tile_s.db_neighbors[2].tile_s.transform.position;
            tpos /= 4;
        }
        tpos.y = transform.position.y;

        db_moves[4].position = tpos; //Tar Tile Marker//
        db_moves[4].gameObject.SetActive(true); //Tar Tile Marker//

        tpos = new Vector3(0, 0, 0);
        if (!big)
        {
            tpos += tar_tile_s.db_path_lowest[num_tile].transform.position;
        }
        else if (big)
        {
            tpos += tar_tile_s.db_path_lowest[num_tile].transform.position + tar_tile_s.db_path_lowest[num_tile].db_neighbors[1].tile_s.transform.position + tar_tile_s.db_path_lowest[num_tile].db_neighbors[2].tile_s.transform.position + tar_tile_s.db_path_lowest[num_tile].db_neighbors[1].tile_s.db_neighbors[2].tile_s.transform.position;
            tpos /= 4;
        }

        tpos.y = transform.position.y;
        db_moves[0].position = tpos;
        db_moves[1].position = tpos;

        moving = true;
        moving_tiles = true;
        body_looking = true;
    }
}
