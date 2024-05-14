using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowBooksOffShelf : IFanAction
{
    [SerializeField] Interaction _interaction;
    [SerializeField] List<PullObjectWithPhysics> _books;
    public override void FanAligned()
    {
        foreach (var book in _books)
        {
            book.Blow();
        }
    }

    public override void FanUnaligned()
    {
        _interaction.EndAction();
    }
}
