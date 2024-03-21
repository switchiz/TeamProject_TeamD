using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Editor�� ��ӹ����� �����Ϳ����� �۵��� , true�� �ڽĵ� �ްڴٴ� ��
[CustomEditor(typeof(EnemyBase_) , true)]
public class EnemyEditor : Editor
{
    // EnemyEditor�� Enemy�� ������ Ŭ�����̹Ƿ� ���� ���õ� Enemy�� ã�ƿü� �־����
    public EnemyBase_ selected;

    // Editor���� OnEnable�� ���� �����Ϳ��� ������Ʈ�� �������� Ȱ��ȭ��
    private void OnEnable()
    {
        // target�� Editor�� �ִ� ������ ������ ������Ʈ�� �޾ƿ�.
        if (AssetDatabase.Contains(target))
        {
            selected = null;
        }
        else
        {
            // target�� Object���̹Ƿ� Enemy�� ����ȯ
            selected = (EnemyBase_)target;
        }
    }

    // ����Ƽ�� �ν����͸� GUI�� �׷��ִ��Լ�
    public override void OnInspectorGUI()
    {
        if (selected == null)
            return;

        Texture2D texture;
        SpriteRenderer spriteRenderer = (selected).GetComponent<SpriteRenderer>();
        EditorGUILayout.LabelField(" ���� ��������Ʈ ");
        texture = SpriteToTexture2D(spriteRenderer.sprite);

        if (texture != null)
        {
            GUILayout.Label("", GUILayout.Height(64), GUILayout.Width(64));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("�����");
        EditorGUILayout.Space();

        GUI.color = Color.white;
        selected.maxHp = EditorGUILayout.IntField("���� ü��", selected.Hp);
        if (selected.maxHp < 0)
            selected.maxHp = 1;

        selected.mobDamage = EditorGUILayout.IntField("���� ���ݷ�", selected.mobDamage);


        selected.IsMove = EditorGUILayout.Toggle("���� �̵� ����", selected.IsMove);

        selected.mobMoveSpeed = EditorGUILayout.FloatField("���� �̵��ӵ�", selected.mobMoveSpeed);
        if (selected.mobMoveSpeed < 0)
            selected.mobMoveSpeed = 0;

        // ��� ���δ�.
        if (GUILayout.Button("���̱�"))
        {
            selected.Die();
        }
    }

    /// <summary>
    /// ��������Ʈ�� �ؽ���2D�� �ٲ��ִ� �޼���
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns></returns>
    Texture2D SpriteToTexture2D(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }
}

